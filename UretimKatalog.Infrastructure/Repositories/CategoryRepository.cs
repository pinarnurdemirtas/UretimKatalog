using System.Linq;
using Microsoft.EntityFrameworkCore;
using UretimKatalog.Domain.Models;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Infrastructure.Data;

namespace UretimKatalog.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public IQueryable<Category> GetSubcategories(int parentId)
            => _context.Set<Category>()
                       .Where(c => c.ParentCategoryId == parentId)
                       .AsNoTracking();
    }
}