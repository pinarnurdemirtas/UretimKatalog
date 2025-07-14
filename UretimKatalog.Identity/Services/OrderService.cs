using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using UretimKatalog.Domain.Models;
using UretimKatalog.Domain.Interfaces;

namespace UretimKatalog.Identity.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.TotalAmount = order.OrderItems.Sum(item => item.Quantity * item.UnitPrice);
            await _uow.Orders.AddAsync(order);
            await _uow.CommitAsync();

            return order.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var order = await _uow.Orders.GetByIdAsync(id)
                         ?? throw new KeyNotFoundException($"Sipariş (ID:{id}) bulunamadı");
            _uow.Orders.Remove(order);
            await _uow.CommitAsync();
        }

    }
}
