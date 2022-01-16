namespace Ordering.Domain.Orders.Mappings;

using Ordering.Dtos.Order;
using AutoMapper;
using Ordering.Domain.Orders;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        //createmap<to this, from this>
        CreateMap<Order, OrderDto>()
            .ReverseMap();
        CreateMap<OrderForCreationDto, Order>();
        CreateMap<OrderForUpdateDto, Order>()
            .ReverseMap();
    }
}