using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _uow.Categories.AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _uow.Categories.GetByIdAsync(id);
            if (entity is not null)
            {
                _uow.Categories.Remove(entity);
                await _uow.CommitAsync();
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var list = _uow.Categories.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(list);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var entity = await _uow.Categories.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(entity!);
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            // DTO'dan entity'ye dönüştür
            var entity = _mapper.Map<Category>(dto);
            // Güncelleme işlemi
            _uow.Categories.Update(entity);
            // Değişiklikleri veritabanına kaydet
            await _uow.CommitAsync();
        }
    }
}
