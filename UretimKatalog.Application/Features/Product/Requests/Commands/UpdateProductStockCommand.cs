using MediatR;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Features.Product.Requests.Commands
{
   public record UpdateProductStockCommand(int Id, int Stock)
                : IRequest<Unit>;
}
