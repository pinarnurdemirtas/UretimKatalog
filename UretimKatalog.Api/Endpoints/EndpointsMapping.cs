using UretimKatalog.Application.Interfaces;
using UretimKatalog.Api.Models;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Api.Endpoints
{
    public static class CategoryEndpoints
    {
        public static RouteGroupBuilder MapCategories(this WebApplication app)
        {
            var group = app.MapGroup("/api/categories").RequireAuthorization();
            group.MapGet("/", async (ICategoryService svc) =>
                Results.Ok(ApiResponse<IEnumerable<CategoryDto>>.Ok(await svc.GetAllAsync())));
            group.MapGet("/{id:int}", async (int id, ICategoryService svc) =>
            {
                var dto = await svc.GetByIdAsync(id);
                return dto is null
                    ? Results.NotFound(ApiResponse<string>.Fail("Kategori bulunamadı"))
                    : Results.Ok(ApiResponse<CategoryDto>.Ok(dto));
            });
            group.MapPost("/", async (CreateCategoryDto dto, ICategoryService svc) =>
                Results.Created($"/api/categories/{(await svc.CreateAsync(dto)).Id}",
                                ApiResponse<CategoryDto>.Ok(await svc.CreateAsync(dto))));
            group.MapPut("/", async (UpdateCategoryDto dto, ICategoryService svc) =>
            {
                await svc.UpdateAsync(dto);
                return Results.Ok(ApiResponse<string>.Ok("Güncellendi"));
            });
            group.MapDelete("/{id:int}", async (int id, ICategoryService svc) =>
            {
                await svc.DeleteAsync(id);
                return Results.Ok(ApiResponse<string>.Ok("Silindi"));
            });
            return group;
        }
    }
}
