namespace Ordering.Domain.OrderLines.Features;

using Ordering.Domain.OrderLines;
using Ordering.Dtos.OrderLine;
using Ordering.Exceptions;
using Ordering.Databases;
using Ordering.Domain.OrderLines.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class AddOrderLine
{
    public class AddOrderLineCommand : IRequest<OrderLineDto>
    {
        public OrderLineForCreationDto OrderLineToAdd { get; set; }

        public AddOrderLineCommand(OrderLineForCreationDto orderLineToAdd)
        {
            OrderLineToAdd = orderLineToAdd;
        }
    }

    public class Handler : IRequestHandler<AddOrderLineCommand, OrderLineDto>
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<OrderLineDto> Handle(AddOrderLineCommand request, CancellationToken cancellationToken)
        {
            var orderLine = _mapper.Map<OrderLine> (request.OrderLineToAdd);
            _db.OrderLines.Add(orderLine);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.OrderLines
                .AsNoTracking()
                .ProjectTo<OrderLineDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(o => o.Id == orderLine.Id, cancellationToken);
        }
    }
}