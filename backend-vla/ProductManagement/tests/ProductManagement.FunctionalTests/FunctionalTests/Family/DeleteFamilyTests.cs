namespace ProductManagement.FunctionalTests.FunctionalTests.Family;

using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteFamilyTests : TestBase
{
    [Test]
    public async Task delete_family_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeFamily = new FakeFamily { }.Generate();
        await InsertAsync(fakeFamily);

        // Act
        var route = ApiRoutes.Familys.Delete.Replace(ApiRoutes.Familys.Id, fakeFamily.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}