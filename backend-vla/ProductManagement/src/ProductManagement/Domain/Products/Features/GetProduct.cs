namespace ProductManagement.Domain.Products.Features;

using ProductManagement.Dtos.Product;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetProduct
{
    public class ProductQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }

        public ProductQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<ProductQuery, ProductDto>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ProductDto> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.Products
                .AsNoTracking()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Product", request.Id);

            return result;
        }
    }
}