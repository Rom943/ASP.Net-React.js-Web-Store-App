using System.ComponentModel.DataAnnotations;

namespace ShopApi.DTO
{
    public class    UpdateProductDTO
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double Price { get; set; } = 0.0;
        public int Stock { get; set; } = 0;
        public IFormFile? ThumbnailImgFile { get; set; }
        public IFormFileCollection? ProductGallery { get; set; }
        
        
    }
}
