namespace Ordering.Domain.OrderLines.Mappings;

using Ordering.Dtos.OrderLine;
using AutoMapper;
using Ordering.Domain.OrderLines;

public class OrderLineProfile : Profile
{
    public OrderLineProfile()
    {
        //createmap<to this, from this>
        CreateMap<OrderLine, OrderLineDto>()
            .ReverseMap();
        CreateMap<OrderLineForCreationDto, OrderLine>();
        CreateMap<OrderLineForUpdateDto, OrderLine>()
            .ReverseMap();
    }
}