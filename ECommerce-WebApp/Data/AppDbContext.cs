using ECommerce_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_WebApp.Data
{
    public class AppDbContext : DbContext
    {
       public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // USERS (1 Admin + 1 Normal)
            // =========================
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@shop.com",
                    PasswordHash = "Admin@123", // hash in real apps
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "john",
                    Email = "john@shop.com",
                    PasswordHash = "User@123",
                    Role = "User"
                }
            );

            // =========================
            // CATEGORIES
            // =========================
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" }
            );

            // =========================
            // BRANDS
            // =========================
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Apple" },
                new Brand { Id = 2, Name = "Nike" }
            );

            // =========================
            // PRODUCTS
            // =========================
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15",
                    Price = 79999,
                    CategoryId = 1,
                    BrandId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Nike Running Shoes",
                    Price = 5999,
                    CategoryId = 2,
                    BrandId = 2
                }
            );
        }


    }
}
