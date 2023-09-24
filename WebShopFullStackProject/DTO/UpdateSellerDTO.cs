namespace ShopApi.DTO
{
    public class UpdateSellerDTO
    {
        public string? StoreName { get; set; }=string.Empty;
        public IFormFile? StoreImage { get; set; }=null;
    }
}
