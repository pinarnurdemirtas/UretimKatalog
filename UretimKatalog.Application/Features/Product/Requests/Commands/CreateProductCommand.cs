using MediatR;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Product.Result;

namespace UretimKatalog.Application.Features.Product.Requests.Commands
{
    public record CreateProductCommand(CreateProductDto Dto) : IRequest<CreateProductResult>;
}
