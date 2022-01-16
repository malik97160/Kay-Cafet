namespace ProductManagement.FunctionalTests.FunctionalTests.Family;

using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateFamilyRecordTests : TestBase
{
    [Test]
    public async Task put_family_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeFamily = new FakeFamily { }.Generate();
        var updatedFamilyDto = new FakeFamilyForUpdateDto { }.Generate();
        await InsertAsync(fakeFamily);

        // Act
        var route = ApiRoutes.Familys.Put.Replace(ApiRoutes.Familys.Id, fakeFamily.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedFamilyDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}