namespace ProductManagement.FunctionalTests.FunctionalTests.Product;

using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetProductTests : TestBase
{
    [Test]
    public async Task get_product_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeProduct = new FakeProduct { }.Generate();
        await InsertAsync(fakeProduct);

        // Act
        var route = ApiRoutes.Products.GetRecord.Replace(ApiRoutes.Products.Id, fakeProduct.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}