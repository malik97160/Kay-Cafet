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

public static class UpdateOrderLine
{
    public class UpdateOrderLineCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public OrderLineForUpdateDto OrderLineToUpdate { get; set; }

        public UpdateOrderLineCommand(Guid orderLine, OrderLineForUpdateDto newOrderLineData)
        {
            Id = orderLine;
            OrderLineToUpdate = newOrderLineData;
        }
    }

    public class Handler : IRequestHandler<UpdateOrderLineCommand, bool>
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateOrderLineCommand request, CancellationToken cancellationToken)
        {
            var orderLineToUpdate = await _db.OrderLines
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (orderLineToUpdate == null)
                throw new NotFoundException("OrderLine", request.Id);

            _mapper.Map(request.OrderLineToUpdate, orderLineToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}