using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Application.Features.Products.Commands;

namespace UretimKatalog.Application.Features.Products.Commands
{
    public record UpdateProductPriceCommand(UpdatePriceDto Dto) : IRequest;

    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand>
    {
        private readonly IUnitOfWork _uow;
        public UpdateProductPriceCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var product = await _uow.Products.GetByIdAsync(dto.Id);
            if (product is not null)
            {
                product.Price = dto.Price;
                await _uow.CommitAsync();
            }
            return Unit.Value;
        }
    }
}
