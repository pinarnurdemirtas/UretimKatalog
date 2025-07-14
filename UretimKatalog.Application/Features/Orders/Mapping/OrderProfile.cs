using AutoMapper;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Application.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.OrderItems,
                           opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());
            CreateMap<CreateOrderItemDto, OrderItem>();
        }
    }
}
