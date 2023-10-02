namespace ShopApi.DTO
{
    public class GetUserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? DOB { get; set; }
        public string ProfileImageSRC { get; set; }=string.Empty;
        
    }
}
