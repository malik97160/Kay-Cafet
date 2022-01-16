namespace Ordering.Domain.Orders.Features;

using Ordering.Domain.Orders;
using Ordering.Dtos.Order;
using Ordering.Exceptions;
using Ordering.Databases;
using Ordering.Domain.Orders.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class AddOrder
{
    public class AddOrderCommand : IRequest<OrderDto>
    {
        public OrderForCreationDto OrderToAdd { get; set; }

        public AddOrderCommand(OrderForCreationDto orderToAdd)
        {
            OrderToAdd = orderToAdd;
        }
    }

    public class Handler : IRequestHandler<AddOrderCommand, OrderDto>
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<OrderDto> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order> (request.OrderToAdd);
            _db.Orders.Add(order);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Orders
                .AsNoTracking()
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(o => o.Id == order.Id, cancellationToken);
        }
    }
}