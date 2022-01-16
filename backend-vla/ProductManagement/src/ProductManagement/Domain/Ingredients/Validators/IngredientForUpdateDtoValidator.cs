namespace ProductManagement.Domain.Ingredients.Validators;

using ProductManagement.Dtos.Ingredient;
using FluentValidation;

public class IngredientForUpdateDtoValidator: IngredientForManipulationDtoValidator<IngredientForUpdateDto>
{
    public IngredientForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}