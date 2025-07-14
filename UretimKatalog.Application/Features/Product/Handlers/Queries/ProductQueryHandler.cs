using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Product.Requests.Queries;
using UretimKatalog.Application.Interfaces;

namespace UretimKatalog.Application.Features.Product.Handlers.Queries
{
    public class ProductQueryHandler :
        IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>,
        IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductService _svc;
        public ProductQueryHandler(IProductService svc) => _svc = svc;

        public Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery q,
                                                    CancellationToken ct)
            => _svc.GetAllAsync();

        public Task<ProductDto> Handle(GetProductByIdQuery q, CancellationToken ct)
            => _svc.GetByIdAsync(q.Id);
    }
}
