// Application/Features/Orders/Requests/Commands/CreateOrderCommand.cs
using MediatR;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Orders.Requests.Commands;
using UretimKatalog.Application.Features.Orders.Responses;

namespace UretimKatalog.Application.Features.Orders.Requests.Commands
{
    public record CreateOrderCommand(CreateOrderDto Dto) 
        : IRequest<CreateOrderResult>;
}
