using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Application.Features.Products.Commands;

namespace UretimKatalog.Application.Features.Products.Commands
{
    public record UpdateProductStockCommand(UpdateStockDto Dto) : IRequest;

    public class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand>
    {
        private readonly IUnitOfWork _uow;
        public UpdateProductStockCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var product = await _uow.Products.GetByIdAsync(dto.Id);
            if (product is not null)
            {
                product.Stock = dto.Stock;
                await _uow.CommitAsync();
            }
            return Unit.Value;
        }
    }
}
