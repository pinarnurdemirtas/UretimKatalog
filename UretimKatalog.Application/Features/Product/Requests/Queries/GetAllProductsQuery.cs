using MediatR;
using System.Collections.Generic;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Features.Product.Requests.Queries
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;
}