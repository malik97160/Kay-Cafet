namespace Ordering.IntegrationTests.FeatureTests.OrderLine;

using Ordering.SharedTestHelpers.Fakes.OrderLine;
using Ordering.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Ordering.Domain.OrderLines.Features;
using static TestFixture;
using Ordering.Exceptions;

public class AddOrderLineCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_orderline_to_db()
    {
        // Arrange
        var fakeOrderLineOne = new FakeOrderLineForCreationDto { }.Generate();

        // Act
        var command = new AddOrderLine.AddOrderLineCommand(fakeOrderLineOne);
        var orderLineReturned = await SendAsync(command);
        var orderLineCreated = await ExecuteDbContextAsync(db => db.OrderLines.SingleOrDefaultAsync());

        // Assert
        orderLineReturned.Should().BeEquivalentTo(fakeOrderLineOne, options =>
            options.ExcludingMissingMembers());
        orderLineCreated.Should().BeEquivalentTo(fakeOrderLineOne, options =>
            options.ExcludingMissingMembers());
    }
}