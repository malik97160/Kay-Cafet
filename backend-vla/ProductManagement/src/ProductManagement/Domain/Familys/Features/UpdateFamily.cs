namespace ProductManagement.Domain.Familys.Features;

using ProductManagement.Domain.Familys;
using ProductManagement.Dtos.Family;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using ProductManagement.Domain.Familys.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class UpdateFamily
{
    public class UpdateFamilyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public FamilyForUpdateDto FamilyToUpdate { get; set; }

        public UpdateFamilyCommand(Guid family, FamilyForUpdateDto newFamilyData)
        {
            Id = family;
            FamilyToUpdate = newFamilyData;
        }
    }

    public class Handler : IRequestHandler<UpdateFamilyCommand, bool>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateFamilyCommand request, CancellationToken cancellationToken)
        {
            var familyToUpdate = await _db.Familys
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (familyToUpdate == null)
                throw new NotFoundException("Family", request.Id);

            _mapper.Map(request.FamilyToUpdate, familyToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}