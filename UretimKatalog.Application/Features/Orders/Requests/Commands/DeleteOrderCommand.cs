using MediatR;

namespace UretimKatalog.Application.Features.Orders.Commands
{
    public record DeleteOrderCommand(int Id) : IRequest<Unit>;
}
