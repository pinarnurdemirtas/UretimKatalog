using System.Collections.Generic;
using System.Threading.Tasks;
using UretimKatalog.Application.Features.Review.Requests.Commands;
using UretimKatalog.Application.Features.Review.Results;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Application.Services
{
    public interface IReviewService
    {
        Task<Review?> GetByIdAsync(int id);
        Task<CreateReviewResult> AddAsync(CreateReviewCommand command);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
        Task<IEnumerable<Review>> GetAllAsync(); 


    }
}
