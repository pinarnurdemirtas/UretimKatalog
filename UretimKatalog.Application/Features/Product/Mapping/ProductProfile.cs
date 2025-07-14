using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Product.Requests.Commands;
using UretimKatalog.Application.Features.Product.Result;
using UretimKatalog.Domain.Models;

public class ProductProfile : Profile
{
      public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();  
            CreateMap<UpdateProductDto, Product>();
            CreateMap<CreateProductCommand, CreateProductDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<Product, CreateProductResult>(); 
            CreateMap<ProductDto, CreateProductResult>();
        }
}
