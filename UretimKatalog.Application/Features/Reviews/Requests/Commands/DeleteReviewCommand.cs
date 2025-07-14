using MediatR;

namespace UretimKatalog.Application.Features.Reviews.Requests.Commands
{
    public record DeleteReviewCommand(int Id) : IRequest<Unit>;
}
