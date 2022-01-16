namespace Ordering.FunctionalTests.FunctionalTests.OrderLine;

using Ordering.SharedTestHelpers.Fakes.OrderLine;
using Ordering.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetOrderLineListTests : TestBase
{
    [Test]
    public async Task get_orderline_list_returns_success()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.OrderLines.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}