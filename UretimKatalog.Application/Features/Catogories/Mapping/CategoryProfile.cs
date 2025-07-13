// ➜ Mapping/CategoryProfile.cs
using AutoMapper;
using UretimKatalog.Domain.Models;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Categories.Requests.Commands;
using UretimKatalog.Application.Features.Categories.Result;

namespace UretimKatalog.Application.Features.Categories.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            /* Command ➜ DTO */
            CreateMap<CreateCategoryCommand, CreateCategoryDto>();
            CreateMap<UpdateCategoryCommand, UpdateCategoryDto>();

            /* DTO ➜ Entity */
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            /* Entity ➜ DTO / Result */
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CreateCategoryResult>();
            
        }
    }
}
