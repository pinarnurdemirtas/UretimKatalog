using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Review.Requests.Commands;
using UretimKatalog.Application.Features.Review.Results;
using UretimKatalog.Application.Features.Reviews.Requests.Commands;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Application.Services;

namespace UretimKatalog.Application.Features.Reviews.Handlers.Commands
{
    public class ReviewCommandHandler :
        IRequestHandler<CreateReviewCommand, CreateReviewResult>,
        IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IReviewService _reviewService;

        public ReviewCommandHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

       public async Task<CreateReviewResult> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            return await _reviewService.AddAsync(request);
        }
       

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            await _reviewService.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
