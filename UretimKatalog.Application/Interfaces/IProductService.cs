using System.Collections.Generic;
using System.Threading.Tasks;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Product.Requests.Commands;
using UretimKatalog.Application.Features.Product.Result;

namespace UretimKatalog.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<CreateProductResult> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(UpdateProductDto dto);
        Task DeleteAsync(int id);
        Task UpdateStockAsync(UpdateStockDto dto);
        Task UpdatePriceAsync(UpdatePriceDto dto);
        Task ToggleStatusAsync(int id);
    }
}
