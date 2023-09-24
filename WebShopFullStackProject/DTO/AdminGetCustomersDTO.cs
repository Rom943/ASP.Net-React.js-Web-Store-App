namespace ShopApi.DTO
{
    public class AdminGetCustomersDTO
    {
        public int ID { get; set; }
        public string ShipingAddress { get; set; } = string.Empty;
        public List<GetCustomerReviewsDTO> Reviews { get; set; }
        public List<GetCustomerPurchasesDTO>? Purchases { get; set; }
        public string? ProfileImgSRC { get; set; }= string.Empty;
    }
}
