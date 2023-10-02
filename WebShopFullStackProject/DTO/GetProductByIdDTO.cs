namespace ShopApi.DTO
{
    public class GetProductByIdDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double Price { get; set; } = 0;
        public double Rank { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public string ThumbnailImgSRC { get; set; }=string.Empty;
        public string[] ProductGallerySRC { get; set; }
        public GetProductDTO[]? RelatedProducts { get; set; }
        public GetCustomerReviewsDTO[] ReviewList { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string StoreImgSRC { get; set; } = string.Empty;

    }
}
