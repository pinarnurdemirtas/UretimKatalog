using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using AutoMapper;
using UretimKatalog.Infrastructure.Services;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;
using Xunit;

namespace UretimKatalog.Tests.Unit.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockUow    = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _service    = new ProductService(_mockUow.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenProductNotFound()
        {
            // Arrange
            var dto = new UpdateProductDto
            {
                Id         = 1,
                Name       = "X",
                Price      = 100m,
                Stock      = 50,
                CategoryId = 2
            };

            // UoW içindeki Products reposunun GetByIdAsync'i null dönsün
            _mockUow
                .Setup(u => u.Products.GetByIdAsync(dto.Id))
                .ReturnsAsync((Product)null);

            // Act & Assert
            await FluentActions
                .Invoking(() => _service.UpdateAsync(dto))
                .Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage("*not found*");  // Veya sizin servisinizde kullanacağınız tam mesaj
        }
    }
}
