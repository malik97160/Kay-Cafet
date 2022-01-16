namespace ProductManagement.FunctionalTests.FunctionalTests.Ingredient;

using ProductManagement.SharedTestHelpers.Fakes.Ingredient;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateIngredientTests : TestBase
{
    [Test]
    public async Task create_ingredient_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeIngredient = new FakeIngredientForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Ingredients.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeIngredient);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}