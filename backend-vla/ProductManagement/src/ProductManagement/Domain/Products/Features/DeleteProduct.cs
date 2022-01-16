namespace ProductManagement.Domain.Products.Features;

using ProductManagement.Domain.Products;
using ProductManagement.Dtos.Product;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid product)
        {
            Id = product;
        }
    }

    public class Handler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Product", request.Id);

            _db.Products.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}