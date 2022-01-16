namespace ProductManagement.Domain.Familys.Features;

using ProductManagement.Dtos.Family;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetFamily
{
    public class FamilyQuery : IRequest<FamilyDto>
    {
        public Guid Id { get; set; }

        public FamilyQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<FamilyQuery, FamilyDto>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<FamilyDto> Handle(FamilyQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.Familys
                .AsNoTracking()
                .ProjectTo<FamilyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Family", request.Id);

            return result;
        }
    }
}