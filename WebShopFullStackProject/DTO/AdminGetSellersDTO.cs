namespace ShopApi.DTO
{
    public class AdminGetSellersDTO
    {
        public int ID { get; set; }
        public string? StoreName { get; set; } = string.Empty;
        public string? StoresImgSRC { get; set; } = string.Empty;
        public List<GetSellerSalesDTO>? SalesDTO { get; set; } = null;
        public List<GetProductDTO>? ProductsDTO { get; set; } = null;
    }
}
