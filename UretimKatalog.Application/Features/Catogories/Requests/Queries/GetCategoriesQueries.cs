using MediatR;
using System.Collections.Generic;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Features.Categories.Requests.Queries
{
    public record GetAllCategoriesQuery()  : IRequest<IEnumerable<CategoryDto>>;
    public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto>;
}