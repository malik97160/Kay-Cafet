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

public class FamilyQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_family_with_accurate_props()
    {
        // Arrange
        var fakeFamilyOne = new FakeFamily { }.Generate();
        await InsertAsync(fakeFamilyOne);

        // Act
        var query = new GetFamily.FamilyQuery(fakeFamilyOne.Id);
        var familys = await SendAsync(query);

        // Assert
        familys.Should().BeEquivalentTo(fakeFamilyOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_family_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetFamily.FamilyQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}