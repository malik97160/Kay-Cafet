namespace Ordering.IntegrationTests.FeatureTests.Order;

using Ordering.SharedTestHelpers.Fakes.Order;
using Ordering.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Ordering.Domain.Orders.Features;
using static TestFixture;
using Ordering.Exceptions;

public class AddOrderCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_order_to_db()
    {
        // Arrange
        var fakeOrderOne = new FakeOrderForCreationDto { }.Generate();

        // Act
        var command = new AddOrder.AddOrderCommand(fakeOrderOne);
        var orderReturned = await SendAsync(command);
        var orderCreated = await ExecuteDbContextAsync(db => db.Orders.SingleOrDefaultAsync());

        // Assert
        orderReturned.Should().BeEquivalentTo(fakeOrderOne, options =>
            options.ExcludingMissingMembers());
        orderCreated.Should().BeEquivalentTo(fakeOrderOne, options =>
            options.ExcludingMissingMembers());
    }
}