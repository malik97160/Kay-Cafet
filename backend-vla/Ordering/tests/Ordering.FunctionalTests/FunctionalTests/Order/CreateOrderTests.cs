namespace Ordering.FunctionalTests.FunctionalTests.Order;

using Ordering.SharedTestHelpers.Fakes.Order;
using Ordering.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateOrderTests : TestBase
{
    [Test]
    public async Task create_order_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeOrder = new FakeOrderForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Orders.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeOrder);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}