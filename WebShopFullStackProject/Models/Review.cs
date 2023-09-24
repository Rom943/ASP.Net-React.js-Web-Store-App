using System.Text.Json.Serialization;

namespace ShopApi.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string? ReviewText { get; set; }
        public double Rank { get; set; } = 0;
        public DateTime Date { get; set; }= DateTime.Now;
        public Customer? Customer { get; set; }
        public Product? Product { get; set; }
    }
}
