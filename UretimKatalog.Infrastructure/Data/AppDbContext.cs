using Microsoft.EntityFrameworkCore;
using UretimKatalog.Infrastructure.Data.Configurations;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            // Seed Data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronik", ParentCategoryId = null },
                new Category { Id = 2, Name = "Kitap",       ParentCategoryId = null }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Telefon", Price = 1500m, Stock = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "Roman",   Price =   50m, Stock = 100, CategoryId = 2 }
            );
        }
    }
}