namespace Ordering.Domain.OrderLines.Features;

using Ordering.Domain.OrderLines;
using Ordering.Dtos.OrderLine;
using Ordering.Exceptions;
using Ordering.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteOrderLine
{
    public class DeleteOrderLineCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteOrderLineCommand(Guid orderLine)
        {
            Id = orderLine;
        }
    }

    public class Handler : IRequestHandler<DeleteOrderLineCommand, bool>
    {
        private readonly OrdersDbContext _db;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteOrderLineCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.OrderLines
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("OrderLine", request.Id);

            _db.OrderLines.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}