namespace ShopApi.DTO
{
    public class GetCustomerReviewsDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ReviewText { get; set; } = string.Empty;
        public double Rank { get; set; } = 0.0;
        public string Date { get; set; }
        public string ReviewerImgSRC { get; set; } = string.Empty;
        public string ReviewerName { get; set; } = string.Empty;
        public string ShipingAddress { get; set; } = string.Empty;
        public string ProductImgSrc { get; set; } = string.Empty;
        
    }
}
