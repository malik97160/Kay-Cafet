namespace ProductManagement.FunctionalTests.FunctionalTests.Ingredient;

using ProductManagement.SharedTestHelpers.Fakes.Ingredient;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateIngredientRecordTests : TestBase
{
    [Test]
    public async Task put_ingredient_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeIngredient = new FakeIngredient { }.Generate();
        var updatedIngredientDto = new FakeIngredientForUpdateDto { }.Generate();
        await InsertAsync(fakeIngredient);

        // Act
        var route = ApiRoutes.Ingredients.Put.Replace(ApiRoutes.Ingredients.Id, fakeIngredient.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedIngredientDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}