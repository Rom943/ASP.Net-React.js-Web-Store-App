namespace ShopApi.DTO
{
    public class GetCategoryListDTO
    {
        public int ID { get; set; }
        public string? CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; } = string.Empty;
        public string? ImageSRC { get; set; } = string.Empty;
        public int ProductCunt { get; set; } = 0;
        public GetProductDTO[]? ProductList { get; set; } = null;
        
    }
}
