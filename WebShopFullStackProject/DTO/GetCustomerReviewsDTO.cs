namespace ShopApi.DTO
{
    public class GetCustomerReviewsDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ReviewText { get; set; }
        public double Rank { get; set; } = 0.0;
        public DateTime Date { get; set; }
        
        
    }
}
