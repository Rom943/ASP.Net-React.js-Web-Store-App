namespace ShopApi.DTO
{
    public class SellerInitialsDTO
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string? StoreName { get; set; } = string.Empty;
        public string? StoresImgSrc { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? UserLastName { get; set; } = string.Empty;
        public string? UserImgSRC { get; set; } = string.Empty;
        public string? UserEmail { get; set; } = string.Empty;  
        public GetProductDTO[]? SellerProducts { get; set; }
        public GetSellerSalesDTO[]? SellerSales { get; set; }
        public GetSellersProductReviewDTO[]? SellersProductsReviews { get; set; }
    }
}
