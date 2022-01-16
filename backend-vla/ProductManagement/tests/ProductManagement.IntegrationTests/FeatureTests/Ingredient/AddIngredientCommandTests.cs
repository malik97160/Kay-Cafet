namespace ProductManagement.IntegrationTests.FeatureTests.Ingredient;

using ProductManagement.SharedTestHelpers.Fakes.Ingredient;
using ProductManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ProductManagement.Domain.Ingredients.Features;
using static TestFixture;
using ProductManagement.Exceptions;

public class AddIngredientCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_ingredient_to_db()
    {
        // Arrange
        var fakeIngredientOne = new FakeIngredientForCreationDto { }.Generate();

        // Act
        var command = new AddIngredient.AddIngredientCommand(fakeIngredientOne);
        var ingredientReturned = await SendAsync(command);
        var ingredientCreated = await ExecuteDbContextAsync(db => db.Ingredients.SingleOrDefaultAsync());

        // Assert
        ingredientReturned.Should().BeEquivalentTo(fakeIngredientOne, options =>
            options.ExcludingMissingMembers());
        ingredientCreated.Should().BeEquivalentTo(fakeIngredientOne, options =>
            options.ExcludingMissingMembers());
    }
}