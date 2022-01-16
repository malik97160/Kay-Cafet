namespace Ordering.IntegrationTests.FeatureTests.Order;

using Ordering.SharedTestHelpers.Fakes.Order;
using Ordering.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Ordering.Domain.Orders.Features;
using static TestFixture;

public class OrderQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_order_with_accurate_props()
    {
        // Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        await InsertAsync(fakeOrderOne);

        // Act
        var query = new GetOrder.OrderQuery(fakeOrderOne.Id);
        var orders = await SendAsync(query);

        // Assert
        orders.Should().BeEquivalentTo(fakeOrderOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_order_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetOrder.OrderQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}