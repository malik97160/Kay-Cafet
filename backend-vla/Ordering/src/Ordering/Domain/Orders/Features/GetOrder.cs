namespace Ordering.Domain.Orders.Features;

using Ordering.Dtos.Order;
using Ordering.Exceptions;
using Ordering.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetOrder
{
    public class OrderQuery : IRequest<OrderDto>
    {
        public Guid Id { get; set; }

        public OrderQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<OrderQuery, OrderDto>
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<OrderDto> Handle(OrderQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.Orders
                .AsNoTracking()
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Order", request.Id);

            return result;
        }
    }
}