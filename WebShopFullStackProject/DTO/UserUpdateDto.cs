using ShopApi.Models.Enums;

namespace ShopApi.DTO
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; } = null;
        public DateTime? Dob { get; set; }
    };
}
