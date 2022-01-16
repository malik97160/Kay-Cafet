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

public static class UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ProductForUpdateDto ProductToUpdate { get; set; }

        public UpdateProductCommand(Guid product, ProductForUpdateDto newProductData)
        {
            Id = product;
            ProductToUpdate = newProductData;
        }
    }

    public class Handler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _db.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (productToUpdate == null)
                throw new NotFoundException("Product", request.Id);

            _mapper.Map(request.ProductToUpdate, productToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}