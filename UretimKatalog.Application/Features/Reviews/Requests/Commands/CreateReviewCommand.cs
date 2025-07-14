using MediatR;
using UretimKatalog.Application.Features.Review.Results;

namespace UretimKatalog.Application.Features.Review.Requests.Commands
{
    public record CreateReviewCommand(int UserId, int ProductId, int Rating, string Comment) 
        : IRequest<CreateReviewResult>;
}
