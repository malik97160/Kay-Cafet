namespace ProductManagement.Domain.Ingredients.Features;

using ProductManagement.Dtos.Ingredient;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetIngredient
{
    public class IngredientQuery : IRequest<IngredientDto>
    {
        public Guid Id { get; set; }

        public IngredientQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<IngredientQuery, IngredientDto>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<IngredientDto> Handle(IngredientQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.Ingredients
                .AsNoTracking()
                .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Ingredient", request.Id);

            return result;
        }
    }
}