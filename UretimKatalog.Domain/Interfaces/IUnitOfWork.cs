using System;
using System.Threading.Tasks;

namespace UretimKatalog.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IProductImageRepository ProductImages { get; }
    IUserRepository Users { get; }
    IOrderRepository Orders { get; }
    Task<int> CommitAsync();
}

}