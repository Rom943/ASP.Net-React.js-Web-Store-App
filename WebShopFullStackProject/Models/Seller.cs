using System.Text.Json.Serialization;

namespace ShopApi.Models
{
    public class Seller
    {
        public int ID { get; set; }
        public string? StoreName { get; set; }=string.Empty;
        public string? StoresImgName { get; set; }=string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; } 
        public List<Product>? Products { get; set; } = null;

        
    }
}
