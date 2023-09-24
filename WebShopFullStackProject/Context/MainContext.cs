using Microsoft.EntityFrameworkCore;
using ShopApi.Models;
using ShopApi.Models.Enums;

namespace ShopApi.Context
{
    public class MainContext:DbContext
    {
        public MainContext(DbContextOptions<MainContext> _options) : base(_options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SiteManager> SiteManagers { get; set; }
        public DbSet<Review> Reveiws { set; get; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasData(
                new User 
                {
                    ID=1,
                    FirstName = "Roma",
                    LastName = "Alexeichick",
                    Password = "Password",
                    Email = "roma19943@gmail.com",
                    DOB= DateTime.Now,
                    UserType = UserType.Admin
                });

        }

        
    }
}
