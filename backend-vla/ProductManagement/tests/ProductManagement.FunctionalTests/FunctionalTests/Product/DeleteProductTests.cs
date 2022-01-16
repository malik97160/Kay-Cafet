namespace ProductManagement.FunctionalTests.FunctionalTests.Product;

using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteProductTests : TestBase
{
    [Test]
    public async Task delete_product_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeProduct = new FakeProduct { }.Generate();
        await InsertAsync(fakeProduct);

        // Act
        var route = ApiRoutes.Products.Delete.Replace(ApiRoutes.Products.Id, fakeProduct.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}