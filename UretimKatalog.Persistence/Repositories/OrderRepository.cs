using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;
using UretimKatalog.Persistence.Data;

namespace UretimKatalog.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) 
            : base(context)
        {
            _context = context;
        }

        // IOrderRepository’den gelen metodu implement ediyoruz:
        public async Task<IEnumerable<Order>> GetByUserAsync(int userId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderItems)
                                 .Where(o => o.UserId == userId)
                                 .ToListAsync();
        }
    }
}
