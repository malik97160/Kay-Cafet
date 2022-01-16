namespace ProductManagement.Domain.Products.Features;

using ProductManagement.Domain.Products;
using ProductManagement.Dtos.Product;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using ProductManagement.Domain.Products.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class AddProduct
{
    public class AddProductCommand : IRequest<ProductDto>
    {
        public ProductForCreationDto ProductToAdd { get; set; }

        public AddProductCommand(ProductForCreationDto productToAdd)
        {
            ProductToAdd = productToAdd;
        }
    }

    public class Handler : IRequestHandler<AddProductCommand, ProductDto>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product> (request.ProductToAdd);
            _db.Products.Add(product);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Products
                .AsNoTracking()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);
        }
    }
}