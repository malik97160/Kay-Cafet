namespace Ordering.Domain.OrderLines.Features;

using Ordering.Domain.OrderLines;
using Ordering.Dtos.OrderLine;
using Ordering.Exceptions;
using Ordering.Databases;
using Ordering.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using System.Threading;
using System.Threading.Tasks;

public static class GetOrderLineList
{
    public class OrderLineListQuery : IRequest<PagedList<OrderLineDto>>
    {
        public OrderLineParametersDto QueryParameters { get; set; }

        public OrderLineListQuery(OrderLineParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<OrderLineListQuery, PagedList<OrderLineDto>>
    {
        private readonly OrdersDbContext _db;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(OrdersDbContext db, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<OrderLineDto>> Handle(OrderLineListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.OrderLines
                as IQueryable<OrderLine>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<OrderLineDto>(_mapper.ConfigurationProvider);

            return await PagedList<OrderLineDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}