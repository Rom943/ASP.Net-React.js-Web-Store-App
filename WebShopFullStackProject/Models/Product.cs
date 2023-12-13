using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShopApi.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double Price { get; set; } = 0;
        public double Rank { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public int? TbnImgId { get; set; }
        public Image? TbnImg { get; set; }
        public int? GalleryId { get; set; }
        public ImageGallery? Gallery { get; set; }
        public List<Review>? Reviews { get; set; }
        public Seller? Seller { get; set; } 
        public List<Purchase>? Purchases { get; set; } = null;
        public List<Cart>? Carts { get; set; } = null;
        public Category? Category { get; set; } = null;

    }
}
