using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Product.Result;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _map;

    public ProductService(IUnitOfWork u, IMapper m) { _uow = u; _map = m; }

    // ProductService
    public async Task<CreateProductResult> CreateAsync(CreateProductDto dto)
        {
            var cat = await _uow.Categories.GetByIdAsync(dto.CategoryId)
                      ?? throw new ArgumentException($"Kategori yok (Id={dto.CategoryId})");

            var entity = _map.Map<Product>(dto);

            await _uow.Products.AddAsync(entity);
            await _uow.CommitAsync();

            return _map.Map<CreateProductResult>(entity);   // mapping sadece burada
        }


    public async Task UpdateAsync(UpdateProductDto dto)
    {
        var entity = await _uow.Products.GetByIdAsync(dto.Id)
                     ?? throw new KeyNotFoundException($"Ürün (ID:{dto.Id}) yok");
        _map.Map(dto, entity);
        await _uow.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ent = await _uow.Products.GetByIdAsync(id)
                  ?? throw new KeyNotFoundException($"Ürün (ID:{id}) yok");
        _uow.Products.Remove(ent);
        await _uow.CommitAsync();
    }

    public async Task UpdatePriceAsync(UpdatePriceDto dto)
    {
        var p = await _uow.Products.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Ürün (ID:{dto.Id}) yok");
        p.Price = dto.Price;
        await _uow.CommitAsync();
    }

    public async Task UpdateStockAsync(UpdateStockDto dto)
    {
        var p = await _uow.Products.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Ürün (ID:{dto.Id}) yok");
        p.Stock = dto.Stock;
        await _uow.CommitAsync();
    }

    public async Task ToggleStatusAsync(int id)
    {
        var p = await _uow.Products.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Ürün (ID:{id}) yok");
        p.IsActive = !p.IsActive;
        await _uow.CommitAsync();
    }

    // Basit sorgular
    public Task<IEnumerable<ProductDto>> GetAllAsync()
        => Task.FromResult(_map.Map<IEnumerable<ProductDto>>(_uow.Products.GetAll()));

    public async Task<ProductDto> GetByIdAsync(int id)
        => _map.Map<ProductDto>(await _uow.Products.GetByIdAsync(id)
                            ?? throw new KeyNotFoundException($"Ürün (ID:{id}) yok"));
}
