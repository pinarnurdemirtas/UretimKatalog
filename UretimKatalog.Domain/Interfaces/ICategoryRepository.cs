using System.Linq;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Domain.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IQueryable<Category> GetSubcategories(int parentId);
            Task<bool> ExistsAsync(int id);   

    }
}