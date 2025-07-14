using System.Threading.Tasks;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Identity.Data;
using UretimKatalog.Persistence.Repositories;

namespace UretimKatalog.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;

        public IProductRepository      Products      { get; }
        public ICategoryRepository     Categories    { get; }
        public IProductImageRepository ProductImages { get; }
        public IUserRepository         Users         { get; }
        public IOrderRepository        Orders        { get; }
        public IReviewRepository       Reviews       { get; } 


        public UnitOfWork(
            AppDbContext ctx,
            IProductRepository prodRepo,
            ICategoryRepository catRepo,
            IProductImageRepository imgRepo,
            IUserRepository userRepo,
            IOrderRepository orderRepo,
            IReviewRepository reviewRepo

        )
        {
            _ctx = ctx;
            Products = prodRepo;
            Categories = catRepo;
            ProductImages = imgRepo;
            Users = userRepo;
            Orders = orderRepo;
            Reviews = reviewRepo;

        }

        public async Task<int> CommitAsync() 
            => await _ctx.SaveChangesAsync();

        public void Dispose() 
            => _ctx.Dispose();
    }
}
