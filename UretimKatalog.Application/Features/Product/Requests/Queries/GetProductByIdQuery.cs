using MediatR;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Features.Product.Requests.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}