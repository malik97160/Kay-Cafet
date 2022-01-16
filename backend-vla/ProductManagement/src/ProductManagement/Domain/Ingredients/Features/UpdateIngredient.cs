namespace ProductManagement.Domain.Ingredients.Features;

using ProductManagement.Domain.Ingredients;
using ProductManagement.Dtos.Ingredient;
using ProductManagement.Exceptions;
using ProductManagement.Databases;
using ProductManagement.Domain.Ingredients.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class UpdateIngredient
{
    public class UpdateIngredientCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public IngredientForUpdateDto IngredientToUpdate { get; set; }

        public UpdateIngredientCommand(Guid ingredient, IngredientForUpdateDto newIngredientData)
        {
            Id = ingredient;
            IngredientToUpdate = newIngredientData;
        }
    }

    public class Handler : IRequestHandler<UpdateIngredientCommand, bool>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientToUpdate = await _db.Ingredients
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (ingredientToUpdate == null)
                throw new NotFoundException("Ingredient", request.Id);

            _mapper.Map(request.IngredientToUpdate, ingredientToUpdate);

            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}