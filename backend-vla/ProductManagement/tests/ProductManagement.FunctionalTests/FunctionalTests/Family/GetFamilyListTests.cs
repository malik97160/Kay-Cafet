namespace ProductManagement.FunctionalTests.FunctionalTests.Family;

using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetFamilyListTests : TestBase
{
    [Test]
    public async Task get_family_list_returns_success()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Familys.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}