using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;
using ShopApi.DTO;

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
        public IActionResult GetByID(int id)
        {
            var result = _userRepo.FindByCondition(u => u.ID == id).FirstOrDefault();
            return Ok(result);
        }

        [HttpPost]
        public async Task <ActionResult<User>> Create([FromForm]UserCreateDto userDTO)

        {
            if (userDTO == null)
            {
                return BadRequest();
            }

            var newUser = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Password = userDTO.Password,
                Email = userDTO.Email,
                UserType = userDTO.UserType,
                DOB = userDTO.DOB,
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


            return Ok();
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

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile , string firstName)
        {
            string imageName =new string (Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            System.IO.Directory.CreateDirectory(_hostEnvironment.ContentRootPath +$"\\Images\\Users\\{firstName}");
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, $"Images\\Users\\{firstName}", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }



    }
}
