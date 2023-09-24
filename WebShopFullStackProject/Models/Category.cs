using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShopApi.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }        
        public string? CategoryName { get; set; }=string.Empty;      
        public string? CategoryDescription { get; set;}=string.Empty;
        public List<Product>? Products { get; set; } = null;
        public SiteManager? SiteManager { get; set; } = null;
        public string? ImageName { get; set; }= string.Empty;


    }
}
