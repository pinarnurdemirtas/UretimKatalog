using Microsoft.EntityFrameworkCore;
using UretimKatalog.Domain.Models;
using UretimKatalog.Infrastructure.Data.Configurations;

namespace UretimKatalog.Infrastructure.Persistence
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}