namespace ProductManagement.FunctionalTests.FunctionalTests.Product;

using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetProductListTests : TestBase
{
    [Test]
    public async Task get_product_list_returns_success()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Products.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}