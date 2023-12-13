using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.DTO;
using ShopApi.Models;
using ShopApi.Models.Enums;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Controllers
{
    [Authorize]
    
    [ApiController]
    [Route("api/initialdata")]
    public class InitialUserController : Controller
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ISellerRepository _sellerRepo;
        private readonly IProductRepository _productRepo;
        private readonly IReviewRepository _reviewRepo;
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        private readonly IImageGalleryRepository _imageGalleryRepository;

        public InitialUserController(
            ICustomerRepository _customerRepo, 
            ISellerRepository _sellerRepo, 
            IProductRepository _productRepo, 
            IReviewRepository _reviewRepo, 
            IPurchaseRepository _purchaseRepo, 
            ICartRepository _cartRepo, 
            IImageService _imageService,
            IImageRepository _imageRepository,
            IImageGalleryRepository _imageGalleryRepository)
        {
            this._customerRepo = _customerRepo;
            this._sellerRepo = _sellerRepo;
            this._productRepo = _productRepo;
            this._reviewRepo = _reviewRepo;
            this._purchaseRepo = _purchaseRepo;
            this._cartRepo = _cartRepo;
            this._imageService = _imageService;
            this._imageRepository = _imageRepository;
            this._imageGalleryRepository = _imageGalleryRepository;
        }
        /// <summary>
        /// Retrieves initial data for a user based on user type and user ID.
        /// </summary>
        /// <param name="userType">Type of the user (customer or seller).</param>
        /// <param name="userTypeId">ID of the user.</param>
        /// <returns>Returns detailed information about the user or a "Not Found" response.</returns>
        [HttpGet("{userType:int}/{userTypeId:int}")]
        public async Task <IActionResult> GetUserTypeInitialsById(int userType,int userTypeId)
        {
            object? user = null;

            if (userType == (int)UserType.Customer)
            {
                user = await GetCustomerByID(userTypeId);
            }
            else if (userType == (int)UserType.Seller)
            {
                user = await GetSellerById(userTypeId);
            }

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }




        /// <summary>
        /// Retrieves detailed information about a customer by ID.
        /// </summary>
        /// <param name="id">ID of the customer.</param>
        /// <returns>Returns a DTO containing customer information.</returns>
        [Authorize(Policy = "CutomerOnly")]
        [NonAction]
        public async Task<CustomerGetDTO> GetCustomerByID(int id)
        {
            var customer = await _customerRepo
            .FindByCondition(c => c.ID == id)
            .Include(c => c.Cart.Products)
            .Include(r => r.Reviews)
            .Include(c => c.Purchases)
            .Include(u=>u.User)
            .Include(iu => iu.User.TbnImg)
            .FirstOrDefaultAsync();
            if (customer == null)
            {
                return null;
            }
            if (customer.Cart == null)
            {
                var cart = new Cart { Customer = customer };
                customer.Cart = await _cartRepo.Create(cart);
            }

            var cartProductsDTO = new List<GetProductToCartDTO>();
            if (customer.Cart.Products != null)
            {
                foreach (var product in customer.Cart.Products)
                {
                    var img = await _imageRepository.FindByCondition(i => i.ID == product.TbnImgId).FirstOrDefaultAsync();
                    var newProductDTO = new GetProductToCartDTO
                    {
                        ID = product.ID,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Stock = product.Stock,
                        ThumbnailImgSrc = img != null? _imageService.GetSingleImageSRC(img,product.ID) : string.Empty,
                    };
                    cartProductsDTO.Add(newProductDTO);
                }
            }
            var reviews = await _reviewRepo.FindByCondition(r => r.Customer.ID == id)
                .Include(r => r.Product)
                .Include(i=>i.Product.TbnImg)
                .ToListAsync();
            var reviewsDto = new List<GetCustomerReviewsDTO>();
            if (reviews != null)
            {
                foreach (var review in reviews)
                {
                    var reviewDTO = new GetCustomerReviewsDTO
                    {
                        ReviewText = review.ReviewText,
                        Rank = review.Rank,
                        ProductName = review.Product.ProductName,
                        ProductId = review.Product.ID,
                        Date = review.Date.ToString(),
                        ProductImgSrc = review.Product.TbnImg != null ? _imageService.GetSingleImageSRC(review.Product.TbnImg,review.Product.ID) : string.Empty,
                    };
                    reviewsDto.Add(reviewDTO);
                }
            }
            var purchases = await _purchaseRepo.FindByCondition(p => p.Customer.ID == customer.ID)
                .Include(p => p.Product)
                .Include(s=>s.Product.Seller)
                .Include(r => r.Product.TbnImg)
                .ToListAsync();
            var purchasesDTO = new List<GetCustomerPurchasesDTO>();
            if (customer.Purchases != null)
            {
                foreach (var purchase in customer.Purchases)
                {
                    var purchaseDTO = new GetCustomerPurchasesDTO
                    {
                        ProductId = purchase.Product.ID,
                        ProductName = purchase.Product.ProductName,
                        Date = purchase.Date.ToString(),
                        TotalPrice = purchase.TotalPrice,
                        SellerName = purchase.Product.Seller.StoreName,
                        ProductImgSrc = purchase.Product.TbnImg != null ? _imageService.GetSingleImageSRC(purchase.Product.TbnImg,purchase.Product.ID): string.Empty,
                    };
                    purchasesDTO.Add(purchaseDTO);
                }
            }

            var cartDto = new CustomerCartDTO { Id = customer.Cart.ID, Products = cartProductsDTO.ToArray() };


            var customerDTO = new CustomerGetDTO
            {
                Id = customer.ID,
                UserId = customer.User.ID,
                ShipingAddress = customer.ShipingAddress,
                UserName = customer.User.FirstName,
                UserLastName = customer.User.LastName,
                UserImgSRC = customer.User.TbnImg !=null ? _imageService.GetSingleImageSRC(customer.User.TbnImg,customer.User.ID) : string.Empty,
                UserEmail = customer.User.Email,
                Cart = cartDto,
                Reviews = reviewsDto.ToArray(),
                Purchases = purchasesDTO.ToArray()
            };

            return customerDTO;
        }




        /// <summary>
        /// Retrieves detailed information about a seller by ID.
        /// </summary>
        /// <param name="id">ID of the seller.</param>
        /// <returns>Returns a DTO containing seller information.</returns>
        [Authorize(Policy = "SellerOnly")]
        [NonAction]
        public async Task<SellerInitialsDTO> GetSellerById(int id)
        {
            var seller = await _sellerRepo.FindByCondition(s => s.ID == id)
                .Include(p => p.Products)
                .Include(u=>u.User)
                .Include(i => i.TbnImg)
                .Include(iu => iu.User.TbnImg)
                .FirstOrDefaultAsync();
            if (seller != null)
            {
                var products = await _productRepo.FindByCondition(p => p.Seller.ID == id).Include(i => i.TbnImg).Include(c => c.Category).ToListAsync();
                List<GetProductDTO> sellerProducts = new List<GetProductDTO>();
                foreach (var product in products)
                {
                    var productDTO = new GetProductDTO
                    {
                        ID = product.ID,
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        Price = product.Price,
                        Rank = Math.Round(product.Rank,1),
                        Stock = product.Stock,
                        Category = product.Category.CategoryName,
                        ThumbnailImgSRC = product.TbnImg != null ? _imageService.GetSingleImageSRC(product.TbnImg, product.ID) : null,
                    };
                    sellerProducts.Add(productDTO);
                }

                var sales = await _purchaseRepo.FindByCondition(s => s.Product.Seller.ID == id)
                    .Include(i => i.Customer.User.TbnImg)
                    .Include(c => c.Customer.User)
                    .Include(p => p.Product)
                    .ToListAsync();

                List<GetSellerSalesDTO> sellerSalesDTO = new List<GetSellerSalesDTO>();

                {
                    foreach (var sale in sales)
                    {
                        var saleDTO = new GetSellerSalesDTO
                        {
                            ProductId = sale.Product.ID,
                            ProductName = sale.Product.ProductName,
                            Date = sale.Date,
                            TotalPrice = sale.TotalPrice,
                            CustomerName = sale.Customer.User.FirstName != null ? sale.Customer.User.FirstName : string.Empty,
                            ShipingAddress = sale.Customer.ShipingAddress != null ? sale.Customer.ShipingAddress : string.Empty,
                            CustomerImgSRC = sale.Customer.User.TbnImg != null ? _imageService.GetSingleImageSRC(sale.Customer.User.TbnImg, sale.Customer.User.ID) : string.Empty,
                        };
                        sellerSalesDTO.Add(saleDTO);
                    }
                }

                var reviews = await _reviewRepo.FindByCondition(r => r.Product.Seller.ID == id)
                    .Include(i => i.Customer.User.TbnImg)
                    .Include(c => c.Customer)
                    .Include(p => p.Product)
                    .ToListAsync();
                List<GetSellersProductReviewDTO> reviewsDTO = new List<GetSellersProductReviewDTO>();
                foreach (var review in reviews)
                {
                    var reviewDTO = new GetSellersProductReviewDTO
                    {
                        ID = review.ID,
                        ReviewText = review.ReviewText,
                        Rank = review.Rank,
                        Date = review.Date,
                        CustomerName = review.Customer.User.FirstName != null ? review.Customer.User.FirstName : string.Empty,
                        ProductName = review.Product.ProductName != null ? review.Product.ProductName : string.Empty,
                        CustomerImgSRC = review.Customer.User.TbnImg != null ? _imageService.GetSingleImageSRC(review.Customer.User.TbnImg, review.Customer.User.ID) : string.Empty,
                    };
                    reviewsDTO.Add(reviewDTO);
                }
                SellerInitialsDTO sellerDTO = new SellerInitialsDTO
                {
                    ID = seller.ID,
                    UserId = seller.User.ID,
                    StoreName = seller.StoreName,
                    StoresImgSrc = seller.TbnImg != null ? _imageService.GetSingleImageSRC(seller.TbnImg, seller.ID) : string.Empty,
                    UserName = seller.User.FirstName,
                    UserLastName = seller.User.LastName,
                    UserImgSRC = seller.User.TbnImg != null ? _imageService.GetSingleImageSRC(seller.User.TbnImg, seller.User.ID) : string.Empty,
                    UserEmail = seller.User.Email,
                    SellerProducts = sellerProducts.ToArray(),
                    SellerSales = sellerSalesDTO.ToArray(),
                    SellersProductsReviews = reviewsDTO.ToArray(),
                };
                return sellerDTO;
            }
            else
            {
                SellerInitialsDTO sellerNullDTO = new SellerInitialsDTO();
                return sellerNullDTO;
            }
        }
    }
}
