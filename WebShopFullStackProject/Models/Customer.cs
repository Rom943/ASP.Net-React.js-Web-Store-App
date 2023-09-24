using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShopApi.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string ShipingAddress { get; set; }=string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Purchase>? Purchases { get; set; } = null;
        public Cart? Cart { get; set; }
        

    }
}
