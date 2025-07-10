using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;
using UretimKatalog.Infrastructure.Data;

namespace UretimKatalog.Infrastructure.Repositories
{
    public class ProductImageRepository 
        : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext ctx) 
            : base(ctx) { }
    }
}
