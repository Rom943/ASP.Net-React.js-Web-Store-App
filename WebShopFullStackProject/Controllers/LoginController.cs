using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ShopApi.Repositories.IRepositories;
using ShopApi.DTO;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        public LoginController(IConfiguration _config, IUserRepository _userRepo)
        {
            this._config = _config ?? throw new ArgumentNullException(nameof(_config));
            this._userRepo = _userRepo ?? throw new ArgumentNullException(nameof(_userRepo)); ;
        }

        [HttpPost]
        public async Task<ActionResult> GetToken(LoginInfo loginInfo)
        {
            if (loginInfo == null)
            {
                return BadRequest();
            }
            var user = await _userRepo.FindByCondition(u => u.Email == loginInfo.Email && u.Password == loginInfo.Password)
                .Include(u=>u.Customer)
                .Include(u=>u.Seller)
                .Include(u=>u.SiteManager)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            var key = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:Secret"] ?? throw new ArgumentException("Authentication:Secret")));
            
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            int userKey = 0;
            switch (user.UserType)
            {
                case Models.Enums.UserType.Customer:
                    userKey = user.Customer.ID;
                    break;
                case Models.Enums.UserType.Seller:
                    userKey = user.Seller.ID;
                    break;
                case Models.Enums.UserType.Admin:
                    userKey = user.SiteManager.ID;
                    break;
            }

            var claims = new List<Claim>()
            {
                new Claim("userType",user.UserType.ToString()),
                new Claim("Key",userKey.ToString())
            };

            string validAudience = _config["Authentication:Audience"] ?? throw new ArgumentException("Authentication:Audience");
            string validIssuer = _config["Authentication:Issuer"] ?? throw new ArgumentException("Authentication:Issuer");

            var token = new JwtSecurityToken(
                validIssuer,
                validAudience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                creds
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenStr);
        }
    }
}
