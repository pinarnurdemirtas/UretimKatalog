using System.Linq;
using Microsoft.EntityFrameworkCore;
using UretimKatalog.Domain.Models;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Identity.Data;

namespace UretimKatalog.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int id)
=> await _context.Categories.AnyAsync(c => c.Id == id);
    }
}