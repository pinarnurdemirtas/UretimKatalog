using MediatR;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Categories.Result;

namespace UretimKatalog.Application.Features.Categories.Requests.Commands
{
    public record CreateCategoryCommand(CreateCategoryDto Dto)
        : IRequest<CreateCategoryResult>;
}