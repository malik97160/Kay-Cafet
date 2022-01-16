namespace ProductManagement.IntegrationTests.FeatureTests.Ingredient;

using ProductManagement.SharedTestHelpers.Fakes.Ingredient;
using ProductManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ProductManagement.Domain.Ingredients.Features;
using static TestFixture;

public class DeleteIngredientCommandTests : TestBase
{
    [Test]
    public async Task can_delete_ingredient_from_db()
    {
        // Arrange
        var fakeIngredientOne = new FakeIngredient { }.Generate();
        await InsertAsync(fakeIngredientOne);
        var ingredient = await ExecuteDbContextAsync(db => db.Ingredients.SingleOrDefaultAsync());
        var id = ingredient.Id;

        // Act
        var command = new DeleteIngredient.DeleteIngredientCommand(id);
        await SendAsync(command);
        var ingredientResponse = await ExecuteDbContextAsync(db => db.Ingredients.ToListAsync());

        // Assert
        ingredientResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_ingredient_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteIngredient.DeleteIngredientCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}