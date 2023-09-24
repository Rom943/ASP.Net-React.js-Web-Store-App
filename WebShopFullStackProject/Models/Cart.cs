using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Models

{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public List<Product>? Products { get; set; } 
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

    }
}
