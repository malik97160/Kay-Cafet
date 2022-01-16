namespace Ordering.FunctionalTests.FunctionalTests.Order;

using Ordering.SharedTestHelpers.Fakes.Order;
using Ordering.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetOrderListTests : TestBase
{
    [Test]
    public async Task get_order_list_returns_success()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Orders.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}