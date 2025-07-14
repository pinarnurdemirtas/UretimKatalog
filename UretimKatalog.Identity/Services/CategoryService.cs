using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Categories.Result;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Identity.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _map;

        public CategoryService(IUnitOfWork uow, IMapper map)
        {
            _uow = uow;
            _map = map;
        }

        public async Task<CreateCategoryResult> CreateAsync(CreateCategoryDto dto)
        {
            var entity = _map.Map<Category>(dto);
            await _uow.Categories.AddAsync(entity);
            await _uow.CommitAsync();

            return _map.Map<CreateCategoryResult>(entity);
        }


        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = await _uow.Categories.GetByIdAsync(dto.Id)
                         ?? throw new KeyNotFoundException($"Kategori (ID:{dto.Id}) yok");
            _map.Map(dto, entity);
            await _uow.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _uow.Categories.GetByIdAsync(id)
                         ?? throw new KeyNotFoundException($"Kategori (ID:{id}) yok");
            _uow.Categories.Remove(entity);
            await _uow.CommitAsync();
        }

        public Task<IEnumerable<CategoryDto>> GetAllAsync()
            => Task.FromResult(_map.Map<IEnumerable<CategoryDto>>(_uow.Categories.GetAll()));

        public async Task<CategoryDto> GetByIdAsync(int id)
            => _map.Map<CategoryDto>(await _uow.Categories.GetByIdAsync(id)
                                    ?? throw new KeyNotFoundException($"Kategori (ID:{id}) yok"));
    }
}