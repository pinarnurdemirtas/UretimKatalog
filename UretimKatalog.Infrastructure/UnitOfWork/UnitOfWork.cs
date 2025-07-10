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
    public IProductImageRepository ProductImages { get; }  // <-- ekli

    public UnitOfWork(
        AppDbContext ctx,
        IProductRepository prodRepo,
        ICategoryRepository catRepo,
        IProductImageRepository imgRepo      // <-- ekli
    )
    {
        _ctx            = ctx;
        Products        = prodRepo;
        Categories      = catRepo;
        ProductImages   = imgRepo;             // <-- atama
    }

    public async Task<int> CommitAsync() 
        => await _ctx.SaveChangesAsync();

    public void Dispose() 
        => _ctx.Dispose();
}


}