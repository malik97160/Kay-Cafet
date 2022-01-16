namespace ProductManagement.Domain.Familys.Features;

using ProductManagement.Domain.Familys;
using ProductManagement.Dtos.Family;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using ProductManagement.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using System.Threading;
using System.Threading.Tasks;

public static class GetFamilyList
{
    public class FamilyListQuery : IRequest<PagedList<FamilyDto>>
    {
        public FamilyParametersDto QueryParameters { get; set; }

        public FamilyListQuery(FamilyParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<FamilyListQuery, PagedList<FamilyDto>>
    {
        private readonly ProductsDbContext _db;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<FamilyDto>> Handle(FamilyListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Familys
                as IQueryable<Family>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<FamilyDto>(_mapper.ConfigurationProvider);

            return await PagedList<FamilyDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}