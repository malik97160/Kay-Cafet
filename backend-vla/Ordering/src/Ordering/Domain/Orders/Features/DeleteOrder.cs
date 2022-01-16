namespace Ordering.Domain.Orders.Features;

using Ordering.Domain.Orders;
using Ordering.Dtos.Order;
using Ordering.Exceptions;
using Ordering.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteOrder
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteOrderCommand(Guid order)
        {
            Id = order;
        }
    }

    public class Handler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Orders
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Order", request.Id);

            _db.Orders.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}