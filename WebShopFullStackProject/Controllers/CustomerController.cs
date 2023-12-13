using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopApi.Models;
using ShopApi.DTO;
using ShopApi.Repositories.IRepositories;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;

namespace ShopApi.Controllers
{
    ////[Authorize]
    ////[Authorize(Policy = "CutomerOnly")]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly IProductRepository _productRepo;
        private readonly IReviewRepository _reviewRepo;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public CustomerController(ICartRepository _cartRepo,
            ICustomerRepository _customerRepo,
            IPurchaseRepository _purchaseRepo,
            IProductRepository _productRepo,
            IReviewRepository _reviewRepo,
            IImageService _imageService,
            IImageRepository _imageRepository)
        {
            this._cartRepo = _cartRepo ?? throw new ArgumentNullException(nameof(_cartRepo)); 
            this._customerRepo = _customerRepo ?? throw new ArgumentNullException(nameof(_customerRepo)); ;
            this._purchaseRepo = _purchaseRepo ?? throw new ArgumentNullException(nameof(_purchaseRepo)); ;
            this._productRepo = _productRepo ?? throw new ArgumentNullException(nameof(_productRepo));
            this._reviewRepo = _reviewRepo ?? throw new ArgumentNullException(nameof(_reviewRepo));
            this._imageService = _imageService ?? throw new ArgumentNullException( nameof(_imageService));
            this._imageRepository = _imageRepository ?? throw new ArgumentNullException(nameof(_imageRepository));
        }

        /// <summary>
        /// Gets Customer initialData by customerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer initial data</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomerByID (int id)
        {
            var customer = await _customerRepo
                .FindByCondition(c=>c.ID == id)
                .Include(c=>c.Cart.Products)
                .Include(r=>r.Reviews)
                .Include(c=>c.Purchases)
                .FirstOrDefaultAsync();
            if(customer == null)
            {
                return BadRequest();
            }
            if (customer.Cart == null)
            {
                var cart = new Cart { Customer = customer };
                customer.Cart = await _cartRepo.Create(cart);
            }

            var productsDTO = new List<GetProductToCartDTO> ();
            if (customer.Cart.Products != null)
            {
                foreach (var product in customer.Cart.Products)
                {
                    var img = await _imageRepository.FindByCondition(i=>i.ID == product.TbnImgId).FirstOrDefaultAsync();
                    var newProductDTO = new GetProductToCartDTO
                    {
                        ID = product.ID,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Stock = product.Stock,
                        ThumbnailImgSrc = img != null ? _imageService.GetSingleImageSRC(img, product.ID) : string.Empty
                    };
                    productsDTO.Add(newProductDTO);
                }
            }
            var reviews = await _reviewRepo.FindByCondition(r => r.Customer.ID == id)
                .Include(i=>i.Product.TbnImg)
                .Include(r=>r.Product).ToListAsync();
            var reviewsDto = new List<GetCustomerReviewsDTO>();
            if(reviews != null) 
            {
                foreach(var review in reviews)
                {
                    var reviewDTO = new GetCustomerReviewsDTO
                    {
                        ReviewText = review.ReviewText,
                        Rank = review.Rank,
                        ProductName = review.Product.ProductName,
                        ProductId = review.Product.ID,
                        ProductImgSrc = review.Product.TbnImg != null ? _imageService.GetSingleImageSRC(review.Product.TbnImg, review.Product.ID) : string.Empty
                    };
                    reviewsDto.Add(reviewDTO);
                }
            }
            var purchases =await _purchaseRepo.FindByCondition(p=>p.Customer.ID==customer.ID)
                .Include(p=>p.Product)
                .Include(i=>i.Product.TbnImg).ToListAsync();
            var purchasesDTO = new List<GetCustomerPurchasesDTO>();
            if(customer.Purchases != null)
            {
                foreach( var purchase in customer.Purchases)
                {
                    var purchaseDTO = new GetCustomerPurchasesDTO
                    {
                        ProductId = purchase.Product.ID,
                        ProductName = purchase.Product.ProductName,
                        Date = purchase.Date.ToString(),
                        TotalPrice = purchase.TotalPrice,
                        ProductImgSrc = purchase.Product.TbnImg != null ? _imageService.GetSingleImageSRC(purchase.Product.TbnImg,purchase.Product.ID) : string.Empty
                    };
                    purchasesDTO.Add(purchaseDTO);
                }
            }
            
            var cartDto = new CustomerCartDTO { Id = customer.Cart.ID,Products=productsDTO.ToArray() };
           

            var customerDTO = new CustomerGetDTO
            {
                Id = customer.ID,
                ShipingAddress = customer.ShipingAddress,
                Cart = cartDto,
                Reviews = reviewsDto.ToArray(),
                Purchases = purchasesDTO.ToArray(),
            };

            return Ok(customerDTO);
        }

        /// <summary>
        /// Gets Customer Cart by customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Customer cart</returns>
        [HttpGet("{customerId:int}/cart")]
        public async Task<ActionResult> GetCustomerCartById(int customerId)
        {
            var cart = await _cartRepo.FindByCondition(c => c.Customer.ID == customerId)                
                .Include(c => c.Products).FirstOrDefaultAsync();
            if(cart == null)
            {
                return NotFound();
            }
            var productsDTO = new List<GetProductToCartDTO>();
            if (cart.Products != null)
            {
                foreach (var product in cart.Products)
                {
                    var img =await _imageRepository.FindByCondition(i=>i.ID == product.TbnImgId).FirstOrDefaultAsync();
                    var newProductDTO = new GetProductToCartDTO
                    {
                        ID = product.ID,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Stock = product.Stock,
                        ThumbnailImgSrc = img != null ? _imageService.GetSingleImageSRC(img, product.ID) : string.Empty
                    };
                    productsDTO.Add(newProductDTO);
                }
            }
            return Ok(productsDTO);

        }


        /// <summary>
        /// Gets Customers Reviews by customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Customer Reviews</returns>
        [HttpGet("{customerId:int}/reviews")]
        public async Task<ActionResult> GetAllCustomerReviews(int customerId)
        {
            var reviews = await _reviewRepo.FindByCondition(r => r.Customer.ID == customerId).Include(r => r.Product).ToListAsync();
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
                        ProductId = review.Product.ID
                    };
                    reviewsDto.Add(reviewDTO);
                }
            }
            return Ok(reviewsDto);
        }


        /// <summary>
        /// Gets Customers Purchases by customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Customer Purchases</returns>
        [HttpGet("{customerId:int}/purchases")]
        public async Task<ActionResult> GetAllCustomerPurchases(int customerId)
        {
            var purchases = await _purchaseRepo.FindByCondition(p=>p.Customer.ID==customerId).Include(p=>p.Product).Include(p=>p.Product.Seller).ToListAsync();
            var purchasesDTO = new List<GetCustomerPurchasesDTO>();
            if (purchases != null)
            {
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
            }
            return Ok(purchasesDTO.ToArray());
        }



        /// <summary>
        /// Adding product to cart by customerId and productId
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns>STATUS 200</returns>
        [HttpPut("cart/{customerId:int}/addProduct/{productId:int}")]
        public async Task<ActionResult<Cart>> AddProductToCart(int customerId, int productId)
        {
            var cart = await _cartRepo.FindByCondition(c => c.CustomerId == customerId).FirstOrDefaultAsync();
            if(cart == null)
            {
                return BadRequest();
            }
            cart.Products = new List<Product>();
            var newProduct = await _productRepo.FindByCondition(p => p.ID == productId).FirstOrDefaultAsync();
            if(newProduct == null) 
            {
                return NotFound();
            }
            cart.Products.Add(newProduct);
            _cartRepo.Update(cart);

            return Ok();
        }



        /// <summary>
        /// Removing product from cart by customerId and customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns>200</returns>
        [HttpPut("cart/{customerId:int}/removeProduct/{productId:int}")]
        public async Task<ActionResult<Cart>> RemoveProductFromCart (int customerId,int productId)
        {
            var cart = await _cartRepo
                .FindByCondition(c=>c.CustomerId==customerId)
                .Include(c=>c.Products)
                .FirstOrDefaultAsync();

            if(cart == null||cart.Products==null)
            {
                return NotFound("there is no products inside the cart or the cart id is invalid");
            }
            
            var product = cart.Products.FirstOrDefault(p=>p.ID==productId);
            if(product == null)
            {
                return NotFound("the product is not inside the cart");
            }

            cart.Products.Remove(product);
            _cartRepo.Update(cart);

            return Ok();    
        }


        /// <summary>
        /// Creating a review for a product using customerId, productId and a reviewDTO
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="customerId"></param>
        /// <param name="review"></param>
        /// <returns>STATUS 200</returns>
        [HttpPost("review/product/{productId:int}/customer/{customerId:int}")]
        public async Task<ActionResult<Review>> AddReviewToProduct(int productId, int customerId, CreateReviewDTO review)
        {
            var customer =await _customerRepo.FindByCondition(c => c.ID == customerId).Include(c => c.Reviews).FirstOrDefaultAsync();
            if(customer == null)
            {
                return NotFound();
            }
            var product = await _productRepo.FindByCondition(p => p.ID == productId).Include(p => p.Reviews).FirstOrDefaultAsync();
            if(product == null)
            {
                return NotFound();
            }

            List<double> rankList = new List<double> ();

            foreach(var rank in product.Reviews)
            {
                rankList.Add(rank.Rank);
            }
            rankList.Add(review.Rank);
            var rankAverage = rankList.Average();

            var newReview = new Review
            {
                ReviewText = review.ReviewText,
                Rank = review.Rank,
                Customer = customer,
                Product = product
            };
            product.Rank = rankAverage;
            _productRepo.Update(product);

            await _reviewRepo.Create(newReview);


            return Ok();
        }

        /// <summary>
        /// Creating a purchase for a product using productId and customerId
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="customerId"></param>
        /// <returns>STATUS 200</returns>

        [HttpPost("purchase/product/{productId:int}/customer/{customerId:int}")]
        public async Task<ActionResult<Purchase>> PurchaseProduct(int productId, int customerId)
        {
            var customer =await _customerRepo.FindByCondition(c => c.ID == customerId).Include(c=>c.Purchases).Include(c=>c.User).FirstOrDefaultAsync();
            if(customer == null) { return NotFound(); }
            var product = await _productRepo.FindByCondition(p => p.ID == productId).Include(p => p.Purchases).FirstOrDefaultAsync();
            if (product == null) { return NotFound(); }
            var purchase = new Purchase
            {
                TotalPrice = product.Price,
                Product = product,
                Customer = customer
            };
            await _purchaseRepo.Create(purchase);
            product.Stock -- ;
            _productRepo.Update(product);
            return Ok();
        }


        /// <summary>
        /// Updates the shipping address for a customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer to update.</param>
        /// <param name="address">The new shipping address.</param>
        /// <returns>Returns 200 OK if the update is successful, 404 Not Found if the customer is not found.</returns>
        [HttpPut("{customerId:int}/update")]
        public async Task <ActionResult> UpdateCustomer(int customerId,[FromForm]string address)
        {
            var customer = await _customerRepo.FindByCondition(c => c.ID == customerId).FirstOrDefaultAsync();
            if(customer==null) 
            {
                return NotFound(); 
            }

            customer.ShipingAddress= address;
            _customerRepo.Update(customer);
            return Ok();
        }

    
    }

}
