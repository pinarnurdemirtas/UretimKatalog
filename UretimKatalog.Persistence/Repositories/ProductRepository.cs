using System.Linq;
using Microsoft.EntityFrameworkCore;
using UretimKatalog.Domain.Models;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Persistence.Data;

namespace UretimKatalog.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public IQueryable<Product> SearchByName(string name)
            => _context.Set<Product>()
                       .Where(p => p.Name.Contains(name))
                       .AsNoTracking();
    }
}