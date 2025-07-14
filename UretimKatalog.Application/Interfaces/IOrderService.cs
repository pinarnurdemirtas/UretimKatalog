using System.Threading.Tasks;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateAsync(CreateOrderDto dto);
        Task DeleteAsync(int id);
    }
}
