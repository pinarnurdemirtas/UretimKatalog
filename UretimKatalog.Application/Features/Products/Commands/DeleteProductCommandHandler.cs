using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Domain.Interfaces;

namespace UretimKatalog.Application.Features.Products.Commands
{
    // Komut: Unit döndüren IRequest
    public record DeleteProductCommand(int Id) : IRequest<Unit>;

    // Handler: IRequestHandler<Komut, Unit> olarak tanımlamalısınız
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteProductCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.Products.GetByIdAsync(request.Id);
            if (entity is not null)
            {
                _uow.Products.Remove(entity);
                await _uow.CommitAsync();
            }
            return Unit.Value;
        }
    }
}
