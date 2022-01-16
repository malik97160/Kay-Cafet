namespace ProductManagement.Domain.Familys.Features;

using ProductManagement.Domain.Familys;
using ProductManagement.Dtos.Family;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteFamily
{
    public class DeleteFamilyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteFamilyCommand(Guid family)
        {
            Id = family;
        }
    }

    public class Handler : IRequestHandler<DeleteFamilyCommand, bool>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteFamilyCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Familys
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Family", request.Id);

            _db.Familys.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}