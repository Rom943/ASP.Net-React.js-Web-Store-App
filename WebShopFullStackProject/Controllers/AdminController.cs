using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.DTO;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Controllers
{
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

        public AdminController(IAdminRepository _adminRepository,
            IUserRepository _userRepository,
            ICustomerRepository _customerRepository,
            ISellerRepository _sellerRepository,
            IPurchaseRepository _purchaseRepository,
            IReviewRepository _reviewRepository,
            ICategoryRepository _categoryRepository,IImageService _imageService)
        {
            this._adminRepository = _adminRepository;
            this._userRepository = _userRepository;
            this._customerRepository = _customerRepository;
            this._sellerRepository = _sellerRepository;
            this._purchaseRepository = _purchaseRepository;
            this._reviewRepository = _reviewRepository;
            this._categoryRepository = _categoryRepository;
            this._imageService = _imageService;
        }

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
                ImageName = _imageService.ImageSaveHandler("categories", categoryDTO.CategoryName, false, categoryDTO.CategoryImgFile, null)
            };

            await _categoryRepository.Create(category);

            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            var customers = _customerRepository.FindAll().Include(c=>c.Reviews).Include(c=>c.Purchases).ToList();
            List<AdminGetCustomersDTO> cusomersList = new List<AdminGetCustomersDTO>();
            foreach (var customer in customers)
            {
                var reviews =await _reviewRepository.FindByCondition(r => r.Customer.ID == customer.ID).Include(r => r.Customer).ToListAsync();
                var purchases = await _purchaseRepository.FindByCondition(p => p.Customer.ID == customer.ID).Include(p => p.Customer).ToListAsync();
                List<GetCustomerReviewsDTO> reviewsDTO = new List<GetCustomerReviewsDTO>();
                List<GetCustomerPurchasesDTO> purchasesDTO = new List<GetCustomerPurchasesDTO>();
                foreach (var purchase in purchases)
                {
                    var purchaseDTO = new GetCustomerPurchasesDTO
                    {
                        ProductId=purchase.Product.ID,
                        ProductName=purchase.Product.ProductName,
                        Date =purchase.Date,
                        TotalPrice=purchase.TotalPrice,
                        SellerName=purchase.Product.Seller.StoreName
                    };
                    purchasesDTO.Add(purchaseDTO);
                }
                foreach(var review in reviews)
                {
                    var reviewDTO = new GetCustomerReviewsDTO
                    {
                        ProductId=review.Product.ID,
                        ProductName=review.Product.ProductName,
                        ReviewText=review.ReviewText,
                        Rank=review.Rank,
                        Date=review.Date,
                    };
                    reviewsDTO.Add(reviewDTO);
                }
                var customerDTO = new AdminGetCustomersDTO
                {
                    ID = customer.ID,
                    ShipingAddress = customer.ShipingAddress,
                    Reviews=reviewsDTO,
                    Purchases=purchasesDTO,
                    ProfileImgSRC=_imageService.GetSingleImageSRC("users",customer.User.FirstName,customer.User.ProfileImageName)
                };
                cusomersList.Add(customerDTO);
            }
            
            return Ok(cusomersList.ToArray());
        }
    }




    
}
