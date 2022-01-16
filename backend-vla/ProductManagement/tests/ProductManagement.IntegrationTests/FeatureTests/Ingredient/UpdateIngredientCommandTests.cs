namespace ProductManagement.IntegrationTests.FeatureTests.Ingredient;

using ProductManagement.SharedTestHelpers.Fakes.Ingredient;
using ProductManagement.IntegrationTests.TestUtilities;
using ProductManagement.Dtos.Ingredient;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ProductManagement.Domain.Ingredients.Features;
using static TestFixture;

public class UpdateIngredientCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_ingredient_in_db()
    {
        // Arrange
        var fakeIngredientOne = new FakeIngredient { }.Generate();
        var updatedIngredientDto = new FakeIngredientForUpdateDto { }.Generate();
        await InsertAsync(fakeIngredientOne);

        var ingredient = await ExecuteDbContextAsync(db => db.Ingredients.SingleOrDefaultAsync());
        var id = ingredient.Id;

        // Act
        var command = new UpdateIngredient.UpdateIngredientCommand(id, updatedIngredientDto);
        await SendAsync(command);
        var updatedIngredient = await ExecuteDbContextAsync(db => db.Ingredients.Where(i => i.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedIngredient.Should().BeEquivalentTo(updatedIngredientDto, options =>
            options.ExcludingMissingMembers());
    }
}