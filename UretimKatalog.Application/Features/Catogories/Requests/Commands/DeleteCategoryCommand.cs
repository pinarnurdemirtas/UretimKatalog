using MediatR;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Categories.Result;

namespace UretimKatalog.Application.Features.Categories.Requests.Commands
{
    public record DeleteCategoryCommand(int Id)                 : IRequest<Unit>;
}