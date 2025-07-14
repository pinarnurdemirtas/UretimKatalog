using System.Collections.Generic;
using System.Threading.Tasks;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Domain.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetByUserAsync(int userId);
    }
}
