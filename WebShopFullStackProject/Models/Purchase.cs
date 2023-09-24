using System.Text.Json.Serialization;

namespace ShopApi.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public double TotalPrice { get; set; } = 0;
        public Product? Product { get; set; }
        public Customer? Customer { get; set; }
    }
}
