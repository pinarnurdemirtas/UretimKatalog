using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Application.Features.Orders.Requests.Commands;
using UretimKatalog.Application.Features.Orders.Responses;
using UretimKatalog.Application.Features.Orders.Commands;

namespace UretimKatalog.Application.Features.Orders.Handlers.Commands
{
    public class OrderCommandHandler
        : IRequestHandler<CreateOrderCommand, CreateOrderResult>,
            IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderService _svc;

        public OrderCommandHandler(IOrderService svc)
            => _svc = svc;

        public async Task<CreateOrderResult> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var newOrderId = await _svc.CreateAsync(request.Dto);
            return new CreateOrderResult { Id = newOrderId };
        }
        public async Task<Unit> Handle(
            DeleteOrderCommand request,
            CancellationToken cancellationToken)
        {
            await _svc.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
