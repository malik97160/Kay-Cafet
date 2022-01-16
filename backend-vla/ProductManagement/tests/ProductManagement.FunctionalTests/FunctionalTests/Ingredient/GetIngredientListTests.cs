namespace ProductManagement.FunctionalTests.FunctionalTests.Ingredient;

using ProductManagement.SharedTestHelpers.Fakes.Ingredient;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetIngredientListTests : TestBase
{
    [Test]
    public async Task get_ingredient_list_returns_success()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Ingredients.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}