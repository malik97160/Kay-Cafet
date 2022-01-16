namespace ProductManagement.Domain.Ingredients.Validators;

using ProductManagement.Dtos.Ingredient;
using FluentValidation;

public class IngredientForCreationDtoValidator: IngredientForManipulationDtoValidator<IngredientForCreationDto>
{
    public IngredientForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}