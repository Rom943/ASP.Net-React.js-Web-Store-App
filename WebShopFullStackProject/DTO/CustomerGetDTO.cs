using ShopApi.Models;

namespace ShopApi.DTO
{
    public class CustomerGetDTO
    {
        public int Id { get; set; }
        public string ShipingAddress { get; set; } = string.Empty;
        public CustomerCartDTO? Cart { get; set; } = null;
        public List<GetCustomerReviewsDTO> Reviews { get; set; }
        public List<GetCustomerPurchasesDTO>? Purchases { get; set; }

    }
}
