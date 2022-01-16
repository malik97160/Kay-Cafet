namespace Ordering.IntegrationTests.FeatureTests.Order;

using Ordering.SharedTestHelpers.Fakes.Order;
using Ordering.IntegrationTests.TestUtilities;
using Ordering.Dtos.Order;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Ordering.Domain.Orders.Features;
using static TestFixture;

public class UpdateOrderCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_order_in_db()
    {
        // Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var updatedOrderDto = new FakeOrderForUpdateDto { }.Generate();
        await InsertAsync(fakeOrderOne);

        var order = await ExecuteDbContextAsync(db => db.Orders.SingleOrDefaultAsync());
        var id = order.Id;

        // Act
        var command = new UpdateOrder.UpdateOrderCommand(id, updatedOrderDto);
        await SendAsync(command);
        var updatedOrder = await ExecuteDbContextAsync(db => db.Orders.Where(o => o.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedOrder.Should().BeEquivalentTo(updatedOrderDto, options =>
            options.ExcludingMissingMembers());
    }
}