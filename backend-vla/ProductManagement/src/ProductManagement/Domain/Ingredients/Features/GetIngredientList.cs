namespace ProductManagement.Domain.Ingredients.Features;

using ProductManagement.Domain.Ingredients;
using ProductManagement.Dtos.Ingredient;
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

public static class GetIngredientList
{
    public class IngredientListQuery : IRequest<PagedList<IngredientDto>>
    {
        public IngredientParametersDto QueryParameters { get; set; }

        public IngredientListQuery(IngredientParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<IngredientListQuery, PagedList<IngredientDto>>
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

        public async Task<PagedList<IngredientDto>> Handle(IngredientListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Ingredients
                as IQueryable<Ingredient>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider);

            return await PagedList<IngredientDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}