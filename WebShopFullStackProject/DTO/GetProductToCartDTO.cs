namespace ShopApi.DTO
{
    public class GetProductToCartDTO
    {
        public int ID { get; set; }
        public string? ProductName { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public string ThumbnailImgSrc { get; set; } = string.Empty;
    }
}
