namespace ShopApi.DTO
{
    public class GetProductDTO
    {
        public int ID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double Price { get; set; } = 0;
        public double Rank { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public string? Category { get; set; }
        public string? ThumbnailImgSRC { get; set; } = null;
        public string[]? ProductGallerySRC { get; set; }
    }
}
