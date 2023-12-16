using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;
using ShopApi.DTO;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        public UserController(
            IUserRepository _userRepo,
            IWebHostEnvironment _hostEnvironment,
            ICustomerRepository _customerRepo,
            IImageService _imageService,
            IImageRepository _imageRepository,
            IProductRepository _productRepository,
            ISellerRepository _sellerRepository,
            ICartRepository _cartRepository,
            IReviewRepository _reviewRepository,
            IImageGalleryRepository _imageGalleryRepository,
            IPurchaseRepository _purchaseRepository)
        {
            this._hostEnvironment = _hostEnvironment ?? throw new ArgumentNullException(nameof(_hostEnvironment));
            this._userRepo = _userRepo ?? throw new ArgumentNullException(nameof(_userRepo));
            this._customerRepo = _customerRepo ?? throw new ArgumentNullException(nameof(_customerRepo));
            this._imageService = _imageService ?? throw new ArgumentNullException(nameof(_imageService));
            this._imageRepository = _imageRepository;
            this._productRepository = _productRepository;
            this._sellerRepository = _sellerRepository;
            this._cartRepository = _cartRepository;
            this._reviewRepository = _reviewRepository;
            this._purchaseRepository = _purchaseRepository;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Returns a list of user details.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userRepo
                .FindAll().Select
                (x => new GetUserDTO()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    DOB = x.DOB,
                })
                .ToList();
            return Ok(result);
        }

        /// <summary>
        /// Gets user details by user ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>Returns details of the specified user.</returns>
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByID(int id)
        {
            var result = await _userRepo.FindByCondition(u => u.ID == id).Include(r => r.Customer).Include(r => r.Seller).Include(i => i.TbnImg).FirstOrDefaultAsync();
            int userId = 0;
            switch (result.UserType)
            {
                case UserType.Customer:
                    userId = result.Customer.ID;
                    break;
                case UserType.Seller:
                    userId = result.Seller.ID;
                    break;
            }
            var userDTO = new GetUserDTO
            {
                ID = result.ID,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                DOB = result.DOB,
                Password = result.Password,
                ProfileImageSRC = result.TbnImg != null ? _imageService.GetSingleImageSRC(result.TbnImg, result.ID) : string.Empty,
                UserType = result.UserType,
                UserTypeId = userId
            };

            return Ok(userDTO);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDTO">The user details to create.</param>
        /// <returns>Returns the ID of the created user.</returns>
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromForm] UserCreateDto userDTO)
        {
            var emailCheck = await _userRepo.FindByCondition(u => u.Email == userDTO.Email).FirstOrDefaultAsync();
            if (emailCheck != null)
            {
                return BadRequest("This email address already exists");
            }
            if (userDTO == null)
            {
                return BadRequest(userDTO);
            }

            var newUser = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Password = userDTO.Password,
                Email = userDTO.Email,
                UserType = userDTO.UserType,
                DOB = userDTO.Dob,
            };

            switch (newUser.UserType)
            {
                case Models.Enums.UserType.Customer:
                    var customer = new Customer { User = newUser };
                    newUser.Customer = customer;
                    break;
                case Models.Enums.UserType.Seller:
                    var seller = new Seller { User = newUser };
                    newUser.Seller = seller;
                    break;
                case Models.Enums.UserType.Admin:
                    var admin = new SiteManager { User = newUser };
                    newUser.SiteManager = admin;
                    break;
            }
            var user = await _userRepo.Create(newUser);
            if (userDTO.ImageFile != null)
            {
                await _imageService.ImageSaveHandler("users", user.ID, userDTO.ImageFile);
            }

            return Ok(user.ID);
        }

        /// <summary>
        /// Updates user details by user ID.
        /// </summary>
        /// <param name="userID">The ID of the user to update.</param>
        /// <param name="userDTO">The updated user details.</param>
        /// <returns>Returns Ok if the operation is successful.</returns>
        [Authorize]
        [HttpPut("update/user/{userID:int}")]
        public async Task<ActionResult> Update(int userID, [FromForm] UserUpdateDto userDTO)
        {
            var user = await _userRepo.FindByCondition(u => u.ID == userID).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;
            user.DOB = userDTO.Dob;

            if (userDTO.ImageFile != null)
            {
                await _imageService.ImageSaveHandler("users", user.ID, userDTO.ImageFile);
            }

            _userRepo.Update(user);
            return Ok();
        }

        /// <summary>
        /// Deletes a user by user ID.
        /// </summary>
        /// <param name="userID">The ID of the user to delete.</param>
        /// <returns>Returns Ok if the operation is successful.</returns>
        [Authorize]
        [HttpDelete("delete/user/{userID:int}")]
        public async Task<ActionResult> Delete(int userID)
        {
            var user = await _userRepo.FindByCondition(u => u.ID == userID)
                .Include(i => i.TbnImg)
                .Include(s => s.Seller)
                .Include(c => c.Customer)
                .Include(a => a.SiteManager)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            if (user.Seller != null)
            {
                var seller = await _sellerRepository.FindByCondition(s => s.UserId == user.ID).Include(p => p.Products).Include(i => i.TbnImg).FirstOrDefaultAsync();
                if (seller != null)
                {
                    List<Product> products = await _productRepository.FindByCondition(p => p.Seller.ID == seller.ID)
                        .Include(i => i.TbnImg)
                        .Include(g => g.Gallery)
                        .Include(p => p.Purchases).ToListAsync();
                    if (products.Count > 0)
                    {
                        foreach (var product in products)
                        {
                            List<Review> reviews = await _reviewRepository.FindByCondition(r => r.Product.ID == product.ID).ToListAsync();
                            if (reviews.Count > 0)
                            {
                                foreach (var review in reviews)
                                {
                                    _reviewRepository.Delete(review);
                                }
                            }
                            List<Purchase> purchases = await _purchaseRepository.FindByCondition(p => p.Product.ID == product.ID).Include(p => p.Product).ToListAsync();
                            if (purchases.Count > 0)
                            {
                                foreach (var purchase in purchases)
                                {
                                    purchase.Product = null;
                                    _purchaseRepository.Update(purchase);
                                }
                            }
                            _productRepository.Delete(product);
                            if (product.TbnImg != null)
                            {
                                _imageService.DeleteImage(product.TbnImg.ID, product.ID);
                            }
                            if (product.Gallery != null)
                            {
                                _imageService.DeleteGallery(product.Gallery.ID, product.ID);
                            }
                        }
                    }

                    _sellerRepository.Delete(seller);
                    if (seller.TbnImg != null)
                    {
                        _imageService.DeleteImage(seller.TbnImg.ID, seller.ID);
                    }
                }
            }

            if (user.Customer != null)
            {
                var customer = await _customerRepo.FindByCondition(c => c.ID == user.Customer.ID)
                    .Include(p => p.Purchases)
                    .Include(r => r.Reviews)
                    .Include(c => c.Cart)
                    .FirstOrDefaultAsync();

                if (customer.Purchases != null)
                {
                    List<Purchase> purchases = await _purchaseRepository.FindByCondition(p => p.Customer.ID == customer.ID).Include(c => c.Customer).ToListAsync();
                    if (purchases.Count > 0)
                    {
                        foreach (var purchase in purchases)
                        {
                            purchase.Customer = null;
                            _purchaseRepository.Update(purchase);
                        }
                    }
                }
                if (customer.Reviews != null)
                {
                    List<Review> reviews = await _reviewRepository.FindByCondition(c => c.Customer.ID == customer.ID).Include(c => c.Customer).ToListAsync();
                    if (reviews.Count > 0)
                    {
                        foreach (var review in reviews)
                        {
                            review.Customer = null;
                            _reviewRepository.Update(review);
                        }
                    }
                }
                if (customer.Cart != null)
                {
                    var cart = await _cartRepository.FindByCondition(c => c.Customer.ID == customer.ID).FirstOrDefaultAsync();
                    if (cart != null)
                    {
                        _cartRepository.Delete(cart);
                    }
                }

                _customerRepo.Delete(customer);
            }

            _userRepo.Delete(user);
            if (user.TbnImg != null)
            {
                _imageService.DeleteImage(user.TbnImg.ID, user.ID);
            }
            return Ok();
        }
    }
}
