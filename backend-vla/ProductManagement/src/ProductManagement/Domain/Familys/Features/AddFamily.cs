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

public static class AddFamily
{
    public class AddFamilyCommand : IRequest<FamilyDto>
    {
        public FamilyForCreationDto FamilyToAdd { get; set; }

        public AddFamilyCommand(FamilyForCreationDto familyToAdd)
        {
            FamilyToAdd = familyToAdd;
        }
    }

    public class Handler : IRequestHandler<AddFamilyCommand, FamilyDto>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<FamilyDto> Handle(AddFamilyCommand request, CancellationToken cancellationToken)
        {
            var family = _mapper.Map<Family> (request.FamilyToAdd);
            _db.Familys.Add(family);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Familys
                .AsNoTracking()
                .ProjectTo<FamilyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(f => f.Id == family.Id, cancellationToken);
        }
    }
}