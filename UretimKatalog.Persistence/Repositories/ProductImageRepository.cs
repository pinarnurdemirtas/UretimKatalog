using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;
using UretimKatalog.Persistence.Data;

namespace UretimKatalog.Persistence.Repositories
{
    public class ProductImageRepository 
        : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext ctx) 
            : base(ctx) { }
    }
}
