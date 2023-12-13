namespace ShopApi.DTO
{
    public class GetSellerSalesDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ShipingAddress { get; set; } = string.Empty;
        public string ProductTbnImgSRC { get; set; } = string.Empty;
        public string CustomerImgSRC { get; set; } = string.Empty;
    }
}
