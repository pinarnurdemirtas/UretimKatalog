using System.Collections.Generic;
using System.Threading.Tasks;
using UretimKatalog.Application.Features.Review.Requests.Commands;
using UretimKatalog.Application.Features.Review.Results;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Application.Services;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _unitOfWork.Reviews.GetAllAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Reviews.GetByIdAsync(id);
        }

        public async Task<CreateReviewResult> AddAsync(CreateReviewCommand command)
        {
            var review = new Review
            {
                UserId = command.UserId,
                ProductId = command.ProductId,
                Rating = command.Rating,
                Comment = command.Comment
            };

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.CommitAsync();

            return new CreateReviewResult
            {
                ReviewId = review.Id,
                Comment = review.Comment
            };
        }

        public async Task UpdateAsync(Review review)
        {
            _unitOfWork.Reviews.Update(review);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review != null)
            {
                _unitOfWork.Reviews.Remove(review);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
