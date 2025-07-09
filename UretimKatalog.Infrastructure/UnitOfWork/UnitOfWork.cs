using System.Threading.Tasks;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Infrastructure.Data;
using UretimKatalog.Infrastructure.Repositories;

namespace UretimKatalog.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _ctx;
        public UnitOfWork(AppDbContext ctx)
        {
            _ctx = ctx;
            Products = new ProductRepository(_ctx);
            Categories = new CategoryRepository(_ctx);
        }
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public async Task<int> CommitAsync() => await _ctx.SaveChangesAsync();
        public void Dispose() => _ctx.Dispose();
    }
}