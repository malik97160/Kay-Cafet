namespace Ordering.FunctionalTests.FunctionalTests.Order;

using Ordering.SharedTestHelpers.Fakes.Order;
using Ordering.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetOrderTests : TestBase
{
    [Test]
    public async Task get_order_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeOrder = new FakeOrder { }.Generate();
        await InsertAsync(fakeOrder);

        // Act
        var route = ApiRoutes.Orders.GetRecord.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}