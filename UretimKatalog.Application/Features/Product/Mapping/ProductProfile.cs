using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Product.Requests.Commands;
using UretimKatalog.Application.Features.Product.Result;
using UretimKatalog.Domain.Models;

public class ProductProfile : Profile
{
      public ProductProfile()
        {
            /* ---------- DTO ➜ Entity ---------- */
            CreateMap<CreateProductDto, Product>();   // alan adları birebir, extra ayar gerekmez
            CreateMap<UpdateProductDto, Product>();

            /* ---------- Command ➜ DTO ---------- */
            CreateMap<CreateProductCommand, CreateProductDto>();

            /* ---------- Entity ➜ DTO ---------- */
            CreateMap<Product, ProductDto>();

            /* ---------- Entity ➜ Result -------- */
            CreateMap<Product, CreateProductResult>();  // ← eksik olan map eklendi

            /* (İsteğe bağlı) DTO ➜ Result */
            CreateMap<ProductDto, CreateProductResult>();
        }
}
