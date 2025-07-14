using System.Linq;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Domain.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
            Task<bool> ExistsAsync(int id);   

    }
}