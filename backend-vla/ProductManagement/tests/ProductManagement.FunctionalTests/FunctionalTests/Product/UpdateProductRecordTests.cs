namespace ProductManagement.FunctionalTests.FunctionalTests.Product;

using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateProductRecordTests : TestBase
{
    [Test]
    public async Task put_product_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeProduct = new FakeProduct { }.Generate();
        var updatedProductDto = new FakeProductForUpdateDto { }.Generate();
        await InsertAsync(fakeProduct);

        // Act
        var route = ApiRoutes.Products.Put.Replace(ApiRoutes.Products.Id, fakeProduct.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedProductDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}