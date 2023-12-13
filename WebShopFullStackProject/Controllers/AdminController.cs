using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.DTO;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;
using System.Diagnostics.Contracts;

namespace ShopApi.Controllers
{
    /// <summary>
    /// Controller for managing administrative tasks.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageService _imageService;
        private readonly IProductRepository _productRepository;

        public AdminController(IAdminRepository _adminRepository,
            IUserRepository _userRepository,
            ICustomerRepository _customerRepository,
            ISellerRepository _sellerRepository,
            IPurchaseRepository _purchaseRepository,
            IReviewRepository _reviewRepository,
            ICategoryRepository _categoryRepository,
            IImageService _imageService,
            IProductRepository _productRepository)
        {
            this._adminRepository = _adminRepository;
            this._userRepository = _userRepository;
            this._customerRepository = _customerRepository;
            this._sellerRepository = _sellerRepository;
            this._purchaseRepository = _purchaseRepository;
            this._reviewRepository = _reviewRepository;
            this._categoryRepository = _categoryRepository;
            this._imageService = _imageService;
            this._productRepository = _productRepository;
        }

        /// <summary>
        /// Creates a new category for products.
        /// </summary>
        /// <param name="adminId">The ID of the admin creating the category.</param>
        /// <param name="categoryDTO">Category details.</param>
        /// <returns>Returns OK if the category is created successfully.</returns>
        [HttpPost("{adminId:int}/category/create")]
        public async Task<ActionResult> CreateCategory(int adminId, [FromForm] CreateCategoryDTO categoryDTO)
        {
            var admin = await _adminRepository.FindByCondition(a => a.ID == adminId).Include(a => a.Categories).FirstOrDefaultAsync();
            if (admin == null)
            {
                return BadRequest();
            }
            if (categoryDTO.CategoryName == null)
            {
                return BadRequest();
            }
            var category = new Category
            {
                CategoryName = categoryDTO.CategoryName,
                CategoryDescription = categoryDTO.CategoryDescription,
                SiteManager = admin,

            };

            var result = await _categoryRepository.Create(category);
            if (categoryDTO.CategoryImgFile != null)
            {
                await _imageService.ImageSaveHandler("categories", result.ID, categoryDTO.CategoryImgFile);
            }
            return Ok();
        }

        /// <summary>
        /// Gets all customers along with their reviews and purchases.
        /// </summary>
        /// <returns>Returns the list of customers with their reviews and purchases.</returns>
        [HttpGet("customers")]
        public async Task<ActionResult> GetAllCustomers()
        {
            var customers = _customerRepository.FindAll().Include(c => c.Reviews).Include(c => c.Purchases).Include(c => c.User).ToList();
            List<AdminGetCustomersDTO> cusomersList = new List<AdminGetCustomersDTO>();
            foreach (var customer in customers)
            {
                var reviews = await _reviewRepository.FindByCondition(r => r.Customer.ID == customer.ID).Include(r => r.Customer).Include(r => r.Product).ToListAsync();
                var purchases = await _purchaseRepository.FindByCondition(p => p.Customer.ID == customer.ID).Include(p => p.Customer).Include(p => p.Product.Seller).ToListAsync();
                List<GetCustomerReviewsDTO> reviewsDTO = new List<GetCustomerReviewsDTO>();
                List<GetCustomerPurchasesDTO> purchasesDTO = new List<GetCustomerPurchasesDTO>();
                foreach (var purchase in purchases)
                {

                    var purchaseDTO = new GetCustomerPurchasesDTO
                    {
                        ProductId = purchase.Product.ID,
                        ProductName = purchase.Product.ProductName,
                        Date = purchase.Date.ToString(),
                        TotalPrice = purchase.TotalPrice,
                        SellerName = purchase.Product.Seller.StoreName
                    };
                    purchasesDTO.Add(purchaseDTO);

                }
                foreach (var review in reviews)
                {
                    string formattedDate = review.Date.ToString("dd.MM.yyyy");
                    var reviewDTO = new GetCustomerReviewsDTO
                    {
                        ProductId = review.Product.ID,
                        ProductName = review.Product.ProductName,
                        ReviewText = review.ReviewText,
                        Rank = review.Rank,
                        Date = formattedDate,
                    };
                    reviewsDTO.Add(reviewDTO);
                }
                var customerDTO = new AdminGetCustomersDTO
                {
                    ID = customer.ID,
                    ShipingAddress = customer.ShipingAddress,
                    Reviews = reviewsDTO,
                    Purchases = purchasesDTO,

                };
                cusomersList.Add(customerDTO);
            }

            return Ok(cusomersList.ToArray());
        }

        /// <summary>
        /// Gets all sellers along with their sales and products.
        /// </summary>
        /// <returns>Returns the list of sellers with their sales and products.</returns>
        [HttpGet("sellers")]
        public async Task<ActionResult> GetAllSellers()
        {
            var sellers = await _sellerRepository.FindAll().Include(s => s.Products).Include(s => s.User).ToListAsync();
            List<AdminGetSellersDTO> sellersListDTO = new List<AdminGetSellersDTO>();
            foreach (var seller in sellers)
            {
                var sales = await _purchaseRepository.FindByCondition(s => s.Product.Seller.ID == seller.ID)
                    .Include(s => s.Customer)
                    .Include(s => s.Customer.User)
                    .Include(s => s.Product)
                    .ToListAsync();
                var products = await _productRepository.FindByCondition(p => p.Seller.ID == seller.ID)
                    .Include(p => p.Category)
                    .ToListAsync();
                List<GetSellerSalesDTO> salesList = new List<GetSellerSalesDTO>();
                foreach (var sale in sales)
                {
                    var saleDTO = new GetSellerSalesDTO
                    {
                        ProductId = sale.Product.ID,
                        ProductName = sale.Product.ProductName,
                        Date = sale.Date,
                        TotalPrice = sale.TotalPrice,
                        CustomerName = sale.Customer.User.FirstName,
                        ShipingAddress = sale.Customer.ShipingAddress
                    };
                    salesList.Add(saleDTO);
                }
                List<GetProductDTO> productList = new List<GetProductDTO>();
                foreach (var product in products)
                {
                    var productsDTO = new GetProductDTO
                    {
                        ID = product.ID,
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        Price = product.Price,
                        Rank = product.Rank,
                        Stock = product.Stock,
                        Category = product.Category.CategoryName,
                    };
                    productList.Add(productsDTO);
                }

                var sellerDTO = new AdminGetSellersDTO
                {
                    ID = seller.ID,
                    StoreName = seller.StoreName,

                    SalesDTO = salesList,
                    ProductsDTO = productList
                };
                sellersListDTO.Add(sellerDTO);

            }
            return Ok(sellersListDTO.ToArray());
        }
    }
}