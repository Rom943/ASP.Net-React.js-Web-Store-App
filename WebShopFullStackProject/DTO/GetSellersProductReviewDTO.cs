namespace ShopApi.DTO
{
    public class GetSellersProductReviewDTO
    {
        public int ID { get; set; }
        public string? ReviewText { get; set; }
        public double Rank { get; set; } = 0;
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
    }
}
