using UretimKatalog.Domain.Models;
using UretimKatalog.Identity.Data;
using UretimKatalog.Persistence.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
    }
}
