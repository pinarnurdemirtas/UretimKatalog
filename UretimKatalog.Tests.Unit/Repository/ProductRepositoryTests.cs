using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UretimKatalog.Infrastructure.Data;        // AppDbContext
using UretimKatalog.Infrastructure.Repositories; // ProductRepository
using UretimKatalog.Domain.Models;              // Product
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

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
        {
            // Arrange
            var ctx = await GetInMemoryContext();
            var repo = new ProductRepository(ctx);

            // Mevcut ürünü al
            var existing = await ctx.Products.FirstAsync();

            // Act
            var result = await repo.GetByIdAsync(existing.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(existing.Id);
            result.Name.Should().Be("Test Ürünü");
        }
    }
}
