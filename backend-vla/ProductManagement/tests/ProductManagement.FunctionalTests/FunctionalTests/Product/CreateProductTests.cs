namespace ProductManagement.FunctionalTests.FunctionalTests.Product;

using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateProductTests : TestBase
{
    [Test]
    public async Task create_product_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeProduct = new FakeProductForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Products.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeProduct);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}