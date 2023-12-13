using System.ComponentModel.DataAnnotations;

namespace ShopApi.Models
{
    public class ImageGallery
    {
        [Key]
        public int ID { get; set; }
        public string? RelatedTo { get; set; }
        public Product Product { get; set; }
        public List<Image> Images { get; set; }
    }
}
