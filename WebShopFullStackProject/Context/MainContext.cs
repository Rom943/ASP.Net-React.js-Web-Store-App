using Microsoft.EntityFrameworkCore;
using ShopApi.Models;
using ShopApi.Models.Enums;

namespace ShopApi.Context
{
    public class MainContext:DbContext
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MainContext(DbContextOptions<MainContext> _options, IWebHostEnvironment _webHostEnvironment) : base(_options) {
            this._webHostEnvironment = _webHostEnvironment;
            Initialize();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SiteManager> SiteManagers { get; set; }
        public DbSet<Review> Reveiws { set; get; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageGallery> ImageGalleries { get; set; }
        public DbSet<SeedingInfo> SeedingInfo { get; set; }

        private string ReadSqlScript()
        {
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "seedingData", "seedScript.sql");
            string sqlScript = File.ReadAllText(filePath);

            // Remove GO statements
            sqlScript = sqlScript.Replace("GO", "");

            return sqlScript;
        }

        private void SeedData()
        {
            // Check if seeding info exists
            var seedingInfo = SeedingInfo.FirstOrDefault();
            if (seedingInfo == null || !seedingInfo.IsSeedingDone)
            {
                string sqlScript = ReadSqlScript();
                Database.ExecuteSqlRaw(sqlScript);

                // Mark seeding as done
                if (seedingInfo == null)
                {
                    SeedingInfo.Add(new SeedingInfo { IsSeedingDone = true });
                }
                else
                {
                    seedingInfo.IsSeedingDone = true;
                    Entry(seedingInfo).State = EntityState.Modified;
                }

                SaveChanges();
            }
        }

        public void Initialize()
        {
            Database.EnsureCreated();
            SeedData();
        }
    }
}

