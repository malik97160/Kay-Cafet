namespace ProductManagement.IntegrationTests.FeatureTests.Family;

using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.IntegrationTests.TestUtilities;
using ProductManagement.Dtos.Family;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ProductManagement.Domain.Familys.Features;
using static TestFixture;

public class UpdateFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_family_in_db()
    {
        // Arrange
        var fakeFamilyOne = new FakeFamily { }.Generate();
        var updatedFamilyDto = new FakeFamilyForUpdateDto { }.Generate();
        await InsertAsync(fakeFamilyOne);

        var family = await ExecuteDbContextAsync(db => db.Familys.SingleOrDefaultAsync());
        var id = family.Id;

        // Act
        var command = new UpdateFamily.UpdateFamilyCommand(id, updatedFamilyDto);
        await SendAsync(command);
        var updatedFamily = await ExecuteDbContextAsync(db => db.Familys.Where(f => f.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedFamily.Should().BeEquivalentTo(updatedFamilyDto, options =>
            options.ExcludingMissingMembers());
    }
}