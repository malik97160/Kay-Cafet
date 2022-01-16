namespace ProductManagement.Domain.Ingredients.Mappings;

using ProductManagement.Dtos.Ingredient;
using AutoMapper;
using ProductManagement.Domain.Ingredients;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        //createmap<to this, from this>
        CreateMap<Ingredient, IngredientDto>()
            .ReverseMap();
        CreateMap<IngredientForCreationDto, Ingredient>();
        CreateMap<IngredientForUpdateDto, Ingredient>()
            .ReverseMap();
    }
}