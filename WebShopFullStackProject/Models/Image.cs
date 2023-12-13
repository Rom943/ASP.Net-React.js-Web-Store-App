namespace ShopApi.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string ImageName { get; set; }
        public string RelatedTo { get; set; }
        public Seller? Seller { get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }
        public Category? Category { get; set; }
        public ImageGallery? Gallery { get; set; } = null;
        
    }
}
