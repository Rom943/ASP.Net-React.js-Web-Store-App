using ShopApi.Models;

namespace ShopApi.DTO
{
    public class CustomerGetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ShipingAddress { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? UserLastName { get; set; } = string.Empty;
        public string? UserImgSRC { get; set; } = string.Empty;
        public string? UserEmail { get; set; } = string.Empty;
        public CustomerCartDTO? Cart { get; set; } = null;
        public GetCustomerReviewsDTO[] Reviews { get; set; }
        public GetCustomerPurchasesDTO[]? Purchases { get; set; }

    }
}
