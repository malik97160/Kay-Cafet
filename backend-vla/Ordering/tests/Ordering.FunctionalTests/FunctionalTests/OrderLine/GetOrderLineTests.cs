namespace Ordering.FunctionalTests.FunctionalTests.OrderLine;

using Ordering.SharedTestHelpers.Fakes.OrderLine;
using Ordering.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetOrderLineTests : TestBase
{
    [Test]
    public async Task get_orderline_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeOrderLine = new FakeOrderLine { }.Generate();
        await InsertAsync(fakeOrderLine);

        // Act
        var route = ApiRoutes.OrderLines.GetRecord.Replace(ApiRoutes.OrderLines.Id, fakeOrderLine.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}