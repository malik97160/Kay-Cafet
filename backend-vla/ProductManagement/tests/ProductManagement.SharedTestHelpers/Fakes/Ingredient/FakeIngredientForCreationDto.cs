namespace ProductManagement.SharedTestHelpers.Fakes.Ingredient;

using AutoBogus;
using ProductManagement.Dtos.Ingredient;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public class FakeIngredientForCreationDto : AutoFaker<IngredientForCreationDto>
{
    public FakeIngredientForCreationDto()
    {
        // if you want default values on any of your properties (e.g. an int between a certain range or a date always in the past), you can add `RuleFor` lines describing those defaults
        //RuleFor(i => i.ExampleIntProperty, i => i.Random.Number(50, 100000));
        //RuleFor(i => i.ExampleDateProperty, i => i.Date.Past());
    }
}