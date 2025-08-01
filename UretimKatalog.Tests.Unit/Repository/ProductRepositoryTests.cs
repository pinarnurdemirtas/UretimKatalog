using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UretimKatalog.Persistence.Data;        
using UretimKatalog.Persistence.Repositories;
using UretimKatalog.Domain.Models;              
using Xunit;

namespace UretimKatalog.Tests.Unit.Repository
{
    public class ProductRepositoryTests
    {
        private async Task<AppDbContext> GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var ctx = new AppDbContext(options);

            // Id artık int
            ctx.Products.Add(new Product
            {
                Id = 1,
                Name = "Test Ürünü",
                Stock = 5,
                Price = 9.99m,
                IsActive = true
            });

            await ctx.SaveChangesAsync();
            return ctx;
        }

      
    }
}
