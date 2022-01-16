namespace ProductManagement.Domain.Products.Features;

using ProductManagement.Domain.Products;
using ProductManagement.Dtos.Product;
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

public static class GetProductList
{
    public class ProductListQuery : IRequest<PagedList<ProductDto>>
    {
        public ProductParametersDto QueryParameters { get; set; }

        public ProductListQuery(ProductParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<ProductListQuery, PagedList<ProductDto>>
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

        public async Task<PagedList<ProductDto>> Handle(ProductListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Products
                as IQueryable<Product>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

            return await PagedList<ProductDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}