using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _uow.Products.AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _uow.Products.GetByIdAsync(id);
            if (entity is not null)
            {
                _uow.Products.Remove(entity);
                await _uow.CommitAsync();
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var list = _uow.Products.GetAll();
            return _mapper.Map<IEnumerable<ProductDto>>(list);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var entity = await _uow.Products.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(entity!);
        }

        public async Task UpdateAsync(UpdateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _uow.Products.Update(entity);
            await _uow.CommitAsync();
        }

        public void UpdateStockAsync(int v1, int v2)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateStockAsync(UpdateStockDto dto)
        {
            var product = await _uow.Products.GetByIdAsync(dto.Id)
                          ?? throw new KeyNotFoundException($"Ürün (ID: {dto.Id}) bulunamadı");
            product.Stock = dto.Stock;
            _uow.Products.Update(product);
            await _uow.CommitAsync();
        }

        public async Task UpdatePriceAsync(UpdatePriceDto dto)
        {
            var product = await _uow.Products.GetByIdAsync(dto.Id)
                          ?? throw new KeyNotFoundException($"Ürün (ID: {dto.Id}) bulunamadı");
            product.Price = dto.Price;
            _uow.Products.Update(product);
            await _uow.CommitAsync();
        }
        public async Task ToggleStatusAsync(int id)
        {
            var product = await _uow.Products.GetByIdAsync(id)
                          ?? throw new KeyNotFoundException($"Ürün (ID: {id}) bulunamadı");

            // Aktifse pasif, pasifse aktif yap
            product.IsActive = !product.IsActive;

            _uow.Products.Update(product);
            await _uow.CommitAsync();
        }
    
    }
}
