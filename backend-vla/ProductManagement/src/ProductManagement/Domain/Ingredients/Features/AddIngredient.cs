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

public static class AddIngredient
{
    public class AddIngredientCommand : IRequest<IngredientDto>
    {
        public IngredientForCreationDto IngredientToAdd { get; set; }

        public AddIngredientCommand(IngredientForCreationDto ingredientToAdd)
        {
            IngredientToAdd = ingredientToAdd;
        }
    }

    public class Handler : IRequestHandler<AddIngredientCommand, IngredientDto>
    {
        private readonly ProductsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(ProductsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<IngredientDto> Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = _mapper.Map<Ingredient> (request.IngredientToAdd);
            _db.Ingredients.Add(ingredient);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Ingredients
                .AsNoTracking()
                .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(i => i.Id == ingredient.Id, cancellationToken);
        }
    }
}