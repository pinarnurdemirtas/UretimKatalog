using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Application.Features.Products.Commands;

namespace UretimKatalog.Application.Features.Products.Commands
{
        public record ToggleProductStatusCommand(int Id) : IRequest;

    public class ToggleProductStatusCommandHandler : IRequestHandler<ToggleProductStatusCommand>
    {
        private readonly IUnitOfWork _uow;
        public ToggleProductStatusCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(ToggleProductStatusCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Products.GetByIdAsync(request.Id);
            if (product is not null)
            {
                product.IsActive = !product.IsActive;
                await _uow.CommitAsync();
            }
            return Unit.Value;
        }
    }
}
