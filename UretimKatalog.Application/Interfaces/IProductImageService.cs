using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Interfaces
{
    public interface IProductImageService
    {
        Task<ProductImageDto> UploadAsync(CreateProductImageDto dto);
    }
}