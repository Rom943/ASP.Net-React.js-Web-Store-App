using ShopApi.Models.Enums;

namespace ShopApi.DTO
{
    public class GetUserDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get;set; } = string.Empty;
        public DateTime? DOB { get; set; }
        public string ProfileImageSRC { get; set; }=string.Empty;
        public UserType UserType { get; set; } 
        public int UserTypeId { get; set; }
    }
}
