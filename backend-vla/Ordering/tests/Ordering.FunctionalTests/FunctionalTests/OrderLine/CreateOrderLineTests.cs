namespace Ordering.FunctionalTests.FunctionalTests.OrderLine;

using Ordering.SharedTestHelpers.Fakes.OrderLine;
using Ordering.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateOrderLineTests : TestBase
{
    [Test]
    public async Task create_orderline_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeOrderLine = new FakeOrderLineForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.OrderLines.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeOrderLine);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}