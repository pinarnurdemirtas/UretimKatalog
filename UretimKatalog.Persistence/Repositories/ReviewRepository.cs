using UretimKatalog.Domain.Models;
using UretimKatalog.Persistence.Data;
using UretimKatalog.Persistence.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
    }
}
