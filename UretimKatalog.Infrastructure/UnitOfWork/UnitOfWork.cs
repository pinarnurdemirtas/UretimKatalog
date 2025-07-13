using System.Threading.Tasks;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Infrastructure.Data;
using UretimKatalog.Infrastructure.Repositories;

namespace UretimKatalog.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;

        public IProductRepository      Products      { get; }
        public ICategoryRepository     Categories    { get; }
        public IProductImageRepository ProductImages { get; }
        public IUserRepository         Users         { get; }

        public UnitOfWork(
            AppDbContext ctx,
            IProductRepository prodRepo,
            ICategoryRepository catRepo,
            IProductImageRepository imgRepo,
            IUserRepository userRepo
        )
        {
            _ctx = ctx;
            Products = prodRepo;
            Categories = catRepo;
            ProductImages = imgRepo;
            Users = userRepo;
        }

        public async Task<int> CommitAsync() 
            => await _ctx.SaveChangesAsync();

        public void Dispose() 
            => _ctx.Dispose();
    }
}
