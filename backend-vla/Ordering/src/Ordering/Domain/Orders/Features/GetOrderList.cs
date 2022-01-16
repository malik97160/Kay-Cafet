namespace Ordering.Domain.Orders.Features;

using Ordering.Domain.Orders;
using Ordering.Dtos.Order;
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

public static class GetOrderList
{
    public class OrderListQuery : IRequest<PagedList<OrderDto>>
    {
        public OrderParametersDto QueryParameters { get; set; }

        public OrderListQuery(OrderParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<OrderListQuery, PagedList<OrderDto>>
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

        public async Task<PagedList<OrderDto>> Handle(OrderListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Orders
                as IQueryable<Order>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider);

            return await PagedList<OrderDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}