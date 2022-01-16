namespace Ordering.FunctionalTests.FunctionalTests.Order;

using Ordering.SharedTestHelpers.Fakes.Order;
using Ordering.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteOrderTests : TestBase
{
    [Test]
    public async Task delete_order_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeOrder = new FakeOrder { }.Generate();
        await InsertAsync(fakeOrder);

        // Act
        var route = ApiRoutes.Orders.Delete.Replace(ApiRoutes.Orders.Id, fakeOrder.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}