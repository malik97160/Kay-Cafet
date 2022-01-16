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

public static class UpdateOrder
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public OrderForUpdateDto OrderToUpdate { get; set; }

        public UpdateOrderCommand(Guid order, OrderForUpdateDto newOrderData)
        {
            Id = order;
            OrderToUpdate = newOrderData;
        }
    }

    public class Handler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _db.Orders
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (orderToUpdate == null)
                throw new NotFoundException("Order", request.Id);

            _mapper.Map(request.OrderToUpdate, orderToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}