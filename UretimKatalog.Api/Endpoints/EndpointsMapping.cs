using UretimKatalog.Application.Interfaces;
using UretimKatalog.Api.Models;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static RouteGroupBuilder MapAuth(this WebApplication app)
        {
            var group = app.MapGroup("/api/auth");
            group.MapPost("/login", async (LoginDto dto, IAuthService auth) =>
            {
                var token = await auth.AuthenticateAsync(dto);
                return Results.Ok(ApiResponse<TokenResponseDto>.Ok(token));
            });
            return group;
        }
    }

    public static class ProductEndpoints
    {
        public static RouteGroupBuilder MapProducts(this WebApplication app)
        {
            var group = app.MapGroup("/api/products").RequireAuthorization();

            group.MapGet("/", async (IProductService svc) =>
                Results.Ok(ApiResponse<IEnumerable<ProductDto>>.Ok(await svc.GetAllAsync())));

            group.MapGet("/{id:int}", async (int id, IProductService svc) =>
            {
                var dto = await svc.GetByIdAsync(id);
                return dto is null
                    ? Results.NotFound(ApiResponse<string>.Fail("Ürün bulunamadı"))
                    : Results.Ok(ApiResponse<ProductDto>.Ok(dto));
            });

            group.MapPost("/", async (CreateProductDto dto, IProductService svc) =>
                Results.Created($"/api/products/{(await svc.CreateAsync(dto)).Id}",
                                ApiResponse<ProductDto>.Ok(await svc.CreateAsync(dto))));

            group.MapPut("/", async (UpdateProductDto dto, IProductService svc) =>
            {
                await svc.UpdateAsync(dto);
                return Results.Ok(ApiResponse<string>.Ok("Güncellendi"));
            });

            group.MapDelete("/{id:int}", async (int id, IProductService svc) =>
            {
                await svc.DeleteAsync(id);
                return Results.Ok(ApiResponse<string>.Ok("Silindi"));
            });

            group.MapPatch("/{id:int}/stock", async (int id, UpdateStockDto dto, IProductService svc) =>
            {
                dto.Id = id;
                await svc.UpdateStockAsync(dto);
                return Results.Ok(ApiResponse<string>.Ok("Stok güncellendi"));
            });

            group.MapPatch("/{id:int}/price", async (int id, UpdatePriceDto dto, IProductService svc) =>
            {
                dto.Id = id;
                await svc.UpdatePriceAsync(dto);
                return Results.Ok(ApiResponse<string>.Ok("Fiyat güncellendi"));
            });

            group.MapPatch("/{id:int}/status", async (int id, IProductService svc) =>
            {
                await svc.ToggleStatusAsync(id);
                return Results.Ok(ApiResponse<string>.Ok("Durum güncellendi"));
            });

            group.MapPost("/{id:int}/images", async (int id, HttpRequest req, IWebHostEnvironment env, IProductImageService imgSvc) =>
            {
                if (!req.Form.Files.Any())
                    return Results.BadRequest(ApiResponse<string>.Fail("Dosya yok"));

                var file = req.Form.Files[0];
                var webRoot = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "wwwroot");
                var dir = Path.Combine(webRoot, "uploads", "products", id.ToString());
                Directory.CreateDirectory(dir);
                var fn = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var fp = Path.Combine(dir, fn);
                await using var fs = new FileStream(fp, FileMode.Create);
                await file.CopyToAsync(fs);
                var url = $"/uploads/products/{id}/{fn}";

                var dto = new CreateProductImageDto { ProductId = id, FileName = fn, Url = url, IsMain = false };
                var created = await imgSvc.UploadAsync(dto);
                return Results.Ok(ApiResponse<ProductImageDto>.Ok(created));
            });

            return group;
        }
    }

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
