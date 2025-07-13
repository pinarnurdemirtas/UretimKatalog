using MediatR;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Features.Product.Requests.Commands
{
    public record DeleteProductCommand(int Id)
                : IRequest<Unit>;
}
