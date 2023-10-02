using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;
using ShopApi.DTO;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IImageService _imageService;

        public UserController(IUserRepository _userRepo,IWebHostEnvironment _hostEnvironment, ICustomerRepository _customerRepo,IImageService _imageService)
        {
            this._hostEnvironment = _hostEnvironment ?? throw new ArgumentNullException(nameof(_hostEnvironment));
            this._userRepo = _userRepo ?? throw new ArgumentNullException(nameof(_userRepo));
            this._customerRepo = _customerRepo?? throw new ArgumentNullException(nameof(_customerRepo));
            this._imageService = _imageService ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userRepo
                .FindAll().Select
                (x => new GetUserDTO()
                {
                    FirstName= x.FirstName,
                    LastName= x.LastName,
                    Email= x.Email,
                    DOB = x.DOB,
                    ProfileImageSRC= _imageService.GetSingleImageSRC("users",x.FirstName,x.ProfileImageName)
                })
                .ToList();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByID(int id)
        {
            var result = await _userRepo.FindByCondition(u => u.ID == id).FirstOrDefaultAsync();

            var userDTO = new GetUserDTO
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                DOB = result.DOB,
                ProfileImageSRC= _imageService.GetSingleImageSRC("Users",result.FirstName,result.ProfileImageName)
            };

            return Ok(userDTO);
        }

        [HttpPost]
        public async Task <ActionResult<User>> Create([FromForm]UserCreateDto userDTO)

        {
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
                ProfileImageName = _imageService.ImageSaveHandler("users",userDTO.FirstName,false,userDTO.ImageFile,null)

            };

            switch (newUser.UserType)
            {
                case Models.Enums.UserType.Customer:
                    var customer = new Customer {User = newUser };
                    newUser.Customer = customer;
                    break;
                case Models.Enums.UserType.Seller:
                    var seller = new Seller {User=newUser};
                    newUser.Seller = seller;
                    break;
                case Models.Enums.UserType.Admin:
                    var admin = new SiteManager { User = newUser};
                    newUser.SiteManager = admin;
                    break;
            }
             await _userRepo.Create(newUser);
            

            return Ok(newUser.ID);
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var exists = _userRepo.FindByCondition(u => u.ID == user.ID).Any();
            if (!exists)
            {
                return NotFound();
            }

            _userRepo.Update(user);

            return NoContent();
        }
        [Authorize]
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var user = _userRepo.FindByCondition(u => u.ID == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            _userRepo.Delete(user);
            return NoContent();
        }






    }
}
