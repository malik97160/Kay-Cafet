namespace Ordering.FunctionalTests.FunctionalTests.OrderLine;

using Ordering.SharedTestHelpers.Fakes.OrderLine;
using Ordering.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteOrderLineTests : TestBase
{
    [Test]
    public async Task delete_orderline_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeOrderLine = new FakeOrderLine { }.Generate();
        await InsertAsync(fakeOrderLine);

        // Act
        var route = ApiRoutes.OrderLines.Delete.Replace(ApiRoutes.OrderLines.Id, fakeOrderLine.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}