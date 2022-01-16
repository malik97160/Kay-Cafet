namespace ProductManagement.FunctionalTests.FunctionalTests.Family;

using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateFamilyTests : TestBase
{
    [Test]
    public async Task create_family_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeFamily = new FakeFamilyForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Familys.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeFamily);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}