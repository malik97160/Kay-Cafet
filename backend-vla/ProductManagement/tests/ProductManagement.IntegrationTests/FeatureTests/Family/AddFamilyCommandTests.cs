namespace ProductManagement.IntegrationTests.FeatureTests.Family;

using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ProductManagement.Domain.Familys.Features;
using static TestFixture;
using ProductManagement.Exceptions;

public class AddFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_family_to_db()
    {
        // Arrange
        var fakeFamilyOne = new FakeFamilyForCreationDto { }.Generate();

        // Act
        var command = new AddFamily.AddFamilyCommand(fakeFamilyOne);
        var familyReturned = await SendAsync(command);
        var familyCreated = await ExecuteDbContextAsync(db => db.Familys.SingleOrDefaultAsync());

        // Assert
        familyReturned.Should().BeEquivalentTo(fakeFamilyOne, options =>
            options.ExcludingMissingMembers());
        familyCreated.Should().BeEquivalentTo(fakeFamilyOne, options =>
            options.ExcludingMissingMembers());
    }
}