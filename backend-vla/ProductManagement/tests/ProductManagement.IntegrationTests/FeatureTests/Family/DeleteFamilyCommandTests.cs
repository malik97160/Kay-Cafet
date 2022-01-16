namespace ProductManagement.IntegrationTests.FeatureTests.Family;

using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ProductManagement.Domain.Familys.Features;
using static TestFixture;

public class DeleteFamilyCommandTests : TestBase
{
    [Test]
    public async Task can_delete_family_from_db()
    {
        // Arrange
        var fakeFamilyOne = new FakeFamily { }.Generate();
        await InsertAsync(fakeFamilyOne);
        var family = await ExecuteDbContextAsync(db => db.Familys.SingleOrDefaultAsync());
        var id = family.Id;

        // Act
        var command = new DeleteFamily.DeleteFamilyCommand(id);
        await SendAsync(command);
        var familyResponse = await ExecuteDbContextAsync(db => db.Familys.ToListAsync());

        // Assert
        familyResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_family_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteFamily.DeleteFamilyCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}