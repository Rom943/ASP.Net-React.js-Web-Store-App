using System.Text.Json.Serialization;

namespace ShopApi.Models
{
    public class SiteManager
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
