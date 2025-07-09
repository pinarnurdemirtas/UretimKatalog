using Microsoft.AspNetCore.Mvc;
using UretimKatalog.Api.Models;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Api.Base;

namespace UretimKatalog.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _svc;
        public CategoriesController(ICategoryService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _svc.GetAllAsync();
            return HandleResult(ApiResponse<IEnumerable<CategoryDto>>.Ok(data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _svc.GetByIdAsync(id);
            if (dto is null)
                return HandleResult(ApiResponse<CategoryDto>.Fail("Kategori bulunamadı"));
            return HandleResult(ApiResponse<CategoryDto>.Ok(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id },
                ApiResponse<CategoryDto>.Ok(created));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            await _svc.UpdateAsync(dto);
            return HandleResult(ApiResponse<string>.Ok("Güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _svc.DeleteAsync(id);
            return HandleResult(ApiResponse<string>.Ok("Silindi"));
        }
    }
}
