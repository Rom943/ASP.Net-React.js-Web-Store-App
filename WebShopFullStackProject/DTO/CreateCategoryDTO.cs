namespace ShopApi.DTO
{
    public class CreateCategoryDTO
    {
        public string? CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; } = string.Empty;
        public IFormFile? CategoryImgFile { get; set; } = null;
    }
}
