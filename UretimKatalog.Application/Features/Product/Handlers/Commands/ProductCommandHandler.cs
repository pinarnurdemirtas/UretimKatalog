using MediatR;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Product.Result;
using UretimKatalog.Application.Features.Product.Requests.Commands;

public class ProductCommandHandler :
    IRequestHandler<CreateProductCommand, CreateProductResult>,
    IRequestHandler<UpdateProductCommand, Unit>,
    IRequestHandler<UpdateProductPriceCommand, Unit>,
    IRequestHandler<UpdateProductStockCommand, Unit>,
    IRequestHandler<ToggleProductStatusCommand, Unit>,
    IRequestHandler<DeleteProductCommand, Unit>
{
        private readonly IProductService _svc;

        public ProductCommandHandler(IProductService svc) => _svc = svc;

        /* ───── CREATE ───── */
        public async Task<CreateProductResult> Handle(CreateProductCommand cmd,
                                                   CancellationToken ct)
        {
                return await _svc.CreateAsync(cmd.Dto);
        }


        /* ───── UPDATE FULL ───── */
        public async Task<Unit> Handle(UpdateProductCommand cmd, CancellationToken ct)
        {
                await _svc.UpdateAsync(cmd.Dto);
                return Unit.Value;
        }

        /* ───── UPDATE PRICE ───── */
        public async Task<Unit> Handle(UpdateProductPriceCommand cmd, CancellationToken ct)
        {
                await _svc.UpdatePriceAsync(new UpdatePriceDto { Id = cmd.Id, Price = cmd.Price });
                return Unit.Value;
        }

        /* ───── UPDATE STOCK ───── */
        public async Task<Unit> Handle(UpdateProductStockCommand cmd, CancellationToken ct)
        {
                await _svc.UpdateStockAsync(new UpdateStockDto { Id = cmd.Id, Stock = cmd.Stock });
                return Unit.Value;
        }

        /* ───── TOGGLE STATUS ───── */
        public async Task<Unit> Handle(ToggleProductStatusCommand cmd, CancellationToken ct)
        {
                await _svc.ToggleStatusAsync(cmd.Id);
                return Unit.Value;
        }

        /* ───── DELETE ───── */
        public async Task<Unit> Handle(DeleteProductCommand cmd, CancellationToken ct)
        {
                await _svc.DeleteAsync(cmd.Id);
                return Unit.Value;
        }
}
