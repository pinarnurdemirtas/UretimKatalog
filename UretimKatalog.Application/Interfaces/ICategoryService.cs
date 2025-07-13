using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Categories.Result;

namespace UretimKatalog.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CreateCategoryResult> CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(UpdateCategoryDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
    }
}
