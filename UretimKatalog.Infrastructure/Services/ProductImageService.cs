using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Infrastructure.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork       _uow;
        private readonly IMapper           _mapper;
        private readonly IHostEnvironment  _env;

        public ProductImageService(
            IUnitOfWork uow,
            IMapper mapper,
            IHostEnvironment env)
        {
            _uow    = uow;
            _mapper = mapper;
            _env    = env;
        }

        public async Task<ProductImageDto> UploadAsync(CreateProductImageDto dto)
        {
            if (!await _uow.Products.ExistsAsync(dto.ProductId))
                throw new KeyNotFoundException($"Ürün (ID: {dto.ProductId}) bulunamadı");

            var wwwroot    = Path.Combine(_env.ContentRootPath, "wwwroot");
            var uploadPath = Path.Combine(wwwroot, "images", "products", dto.ProductId.ToString());
            Directory.CreateDirectory(uploadPath);

            var ext      = Path.GetExtension(dto.File.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(uploadPath, fileName);
            await using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var relativeUrl = $"/images/products/{dto.ProductId}/{fileName}";
            var entity = new ProductImage(dto.ProductId, fileName, relativeUrl, dto.IsMain);

            await _uow.ProductImages.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<ProductImageDto>(entity);
        }
    }
}
