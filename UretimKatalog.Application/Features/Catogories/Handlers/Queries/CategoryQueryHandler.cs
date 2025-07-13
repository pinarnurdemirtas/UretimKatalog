// ➜ Handlers/Queries/CategoryQueryHandler.cs
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Application.Features.Categories.Requests.Queries;

public class CategoryQueryHandler :
    IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>,
    IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryService _svc;
    public CategoryQueryHandler(ICategoryService svc) => _svc = svc;

    public Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery q, CancellationToken ct)
        => _svc.GetAllAsync();

    public Task<CategoryDto> Handle(GetCategoryByIdQuery q, CancellationToken ct)
        => _svc.GetByIdAsync(q.Id);
}
