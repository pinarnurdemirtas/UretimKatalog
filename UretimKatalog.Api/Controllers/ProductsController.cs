using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UretimKatalog.Api.Base;
using UretimKatalog.Api.Models;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;

namespace UretimKatalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IProductService _svc;
        private readonly IProductImageService _imageSvc;
        private readonly IWebHostEnvironment _env;

        public ProductsController(
            IProductService svc,
            IProductImageService imageSvc,
            IWebHostEnvironment env)
        {
            _svc      = svc;
            _imageSvc = imageSvc;
            _env      = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _svc.GetAllAsync();
            return HandleResult(ApiResponse<IEnumerable<ProductDto>>.Ok(data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _svc.GetByIdAsync(id);
            if (dto is null)
                return HandleResult(ApiResponse<ProductDto>.Fail("Ürün bulunamadı"));
            return HandleResult(ApiResponse<ProductDto>.Ok(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id },
                ApiResponse<ProductDto>.Ok(created));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto dto)
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

        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto dto)
        {
            dto.Id = id;
            await _svc.UpdateStockAsync(dto);
            return HandleResult(ApiResponse<string>.Ok("Stok güncellendi"));
        }

        [HttpPatch("{id}/price")]
        public async Task<IActionResult> UpdatePrice(int id, [FromBody] UpdatePriceDto dto)
        {
            dto.Id = id;
            await _svc.UpdatePriceAsync(dto);
            return HandleResult(ApiResponse<string>.Ok("Fiyat güncellendi"));
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _svc.ToggleStatusAsync(id);
            return HandleResult(ApiResponse<string>.Ok("Ürün durumu güncellendi"));
        }

[HttpPost("{id}/images")]
public async Task<IActionResult> UploadImage(int id, IFormFile file)
{
    try
    {
        if (file == null || file.Length == 0)
            return HandleResult(ApiResponse<string>.Fail("Dosya seçilmedi"));

        // wwwroot kökünü al, yoksa ContentRootPath/wwwroot klasörüne fallback yap
        var webRoot = _env.WebRootPath;
        if (string.IsNullOrEmpty(webRoot))
            webRoot = Path.Combine(_env.ContentRootPath, "wwwroot");

        // uploads/products/{id} klasörünü oluştur
        var uploadDir = Path.Combine(webRoot, "uploads", "products", id.ToString());
        Directory.CreateDirectory(uploadDir);

        // Benzersiz dosya adı oluştur
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadDir, fileName);

        // Dosyayı kaydet
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Erişim URL’si
        var url = $"/uploads/products/{id}/{fileName}";

        // DTO’yu oluştur ve servis katmanına aktar
        var imageDto = new CreateProductImageDto
        {
            ProductId = id,
            FileName  = fileName,
            Url       = url,
            IsMain    = false
        };

        var created = await _imageSvc.UploadAsync(imageDto);
        return HandleResult(ApiResponse<ProductImageDto>.Ok(created));
    }
    catch (KeyNotFoundException knf)
    {
        // Ürün bulunamadı
        return HandleResult(ApiResponse<string>.Fail(knf.Message));
    }
    catch (Exception ex)
    {
        // Geliştirme ortamında detay, üretimde genel mesaj
        var msg = _env.IsDevelopment() ? ex.ToString() : "Resim yükleme sırasında bir hata oluştu";
        return HandleResult(ApiResponse<string>.Fail(msg));
    }
}

    }
}
