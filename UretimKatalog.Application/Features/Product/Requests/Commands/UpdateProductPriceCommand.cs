using MediatR;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Features.Product.Requests.Commands
{
   public record UpdateProductPriceCommand(int Id, decimal Price)
                : IRequest<Unit>;
}
