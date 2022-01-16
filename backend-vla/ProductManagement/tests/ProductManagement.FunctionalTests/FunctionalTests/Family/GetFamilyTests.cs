namespace ProductManagement.FunctionalTests.FunctionalTests.Family;

using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetFamilyTests : TestBase
{
    [Test]
    public async Task get_family_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeFamily = new FakeFamily { }.Generate();
        await InsertAsync(fakeFamily);

        // Act
        var route = ApiRoutes.Familys.GetRecord.Replace(ApiRoutes.Familys.Id, fakeFamily.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}