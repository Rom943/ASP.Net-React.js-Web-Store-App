using ShopApi.Models;

namespace ShopApi.DTO
{
    public class CustomerCartDTO
    {
        public int Id { get; set; }
        public List<GetProductToCartDTO>? Products { get; set; }
    }
}
