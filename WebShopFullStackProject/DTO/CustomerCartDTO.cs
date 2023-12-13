using ShopApi.Models;

namespace ShopApi.DTO
{
    public class CustomerCartDTO
    {
        public int Id { get; set; }
        public GetProductToCartDTO[]? Products { get; set; }
    }
}
