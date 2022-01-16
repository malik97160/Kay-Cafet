namespace Ordering.Domain.OrderLines.Features;

using Ordering.Dtos.OrderLine;
using Ordering.Exceptions;
using Ordering.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetOrderLine
{
    public class OrderLineQuery : IRequest<OrderLineDto>
    {
        public Guid Id { get; set; }

        public OrderLineQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<OrderLineQuery, OrderLineDto>
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<OrderLineDto> Handle(OrderLineQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.OrderLines
                .AsNoTracking()
                .ProjectTo<OrderLineDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("OrderLine", request.Id);

            return result;
        }
    }
}