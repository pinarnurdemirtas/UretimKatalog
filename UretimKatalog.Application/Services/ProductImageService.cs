using System;
using System.Threading.Tasks;
using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper     _mapper;

        public ProductImageService(IUnitOfWork uow, IMapper mapper)
        {
            _uow    = uow;
            _mapper = mapper;
        }

        public async Task<ProductImageDto> UploadAsync(CreateProductImageDto dto)
        {
            // 1) Ürünün varlığını kontrol et
            var product = await _uow.Products.GetByIdAsync(dto.ProductId)
                          ?? throw new KeyNotFoundException($"Ürün (ID: {dto.ProductId}) bulunamadı");

            // 2) Yeni ProductImage örneği oluştur
            var entity = new ProductImage(
                dto.ProductId,   // int Id
                dto.FileName,
                dto.Url,
                dto.IsMain
            );

            // 3) EF Core change tracker’a ekle ve kaydet
            await _uow.ProductImages.AddAsync(entity);
            await _uow.CommitAsync();

            // 4) DTO’ya çevirip dön
            return _mapper.Map<ProductImageDto>(entity);
        }
    }
}
