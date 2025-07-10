using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<ProductImage, ProductImageDto>();
        CreateMap<CreateProductImageDto, ProductImage>();
        }
    }
}
