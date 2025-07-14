using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Application.Features.Product.Requests.Commands;
using UretimKatalog.Application.Features.Product.Result;

namespace UretimKatalog.Application.Features.Product.Handlers.Commands
{
    public class ProductCommandHandler :
        IRequestHandler<CreateProductCommand, CreateProductResult>,
        IRequestHandler<UpdateProductCommand, Unit>,
        IRequestHandler<UpdateProductPriceCommand, Unit>,
        IRequestHandler<UpdateProductStockCommand, Unit>,
        IRequestHandler<ToggleProductStatusCommand, Unit>,
        IRequestHandler<DeleteProductCommand, Unit>
        {
        private readonly IProductService      _svc;
        private readonly IProductImageService _imgSvc;

        public ProductCommandHandler(
            IProductService svc,
            IProductImageService imgSvc)
        {
            _svc    = svc;
            _imgSvc = imgSvc;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            => await _svc.CreateAsync(request.Dto);

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _svc.UpdateAsync(request.Dto);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            await _svc.UpdatePriceAsync(new UpdatePriceDto 
            { 
                Id    = request.Id, 
                Price = request.Price 
            });
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            await _svc.UpdateStockAsync(new UpdateStockDto 
            { 
                Id    = request.Id, 
                Stock = request.Stock 
            });
            return Unit.Value;
        }

        public async Task<Unit> Handle(ToggleProductStatusCommand request, CancellationToken cancellationToken)
        {
            await _svc.ToggleStatusAsync(request.Id);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _svc.DeleteAsync(request.Id);
            return Unit.Value;
        }

        
    }
}
