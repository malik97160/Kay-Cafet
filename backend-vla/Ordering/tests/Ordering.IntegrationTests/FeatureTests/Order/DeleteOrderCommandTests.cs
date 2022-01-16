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

public class DeleteOrderCommandTests : TestBase
{
    [Test]
    public async Task can_delete_order_from_db()
    {
        // Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        await InsertAsync(fakeOrderOne);
        var order = await ExecuteDbContextAsync(db => db.Orders.SingleOrDefaultAsync());
        var id = order.Id;

        // Act
        var command = new DeleteOrder.DeleteOrderCommand(id);
        await SendAsync(command);
        var orderResponse = await ExecuteDbContextAsync(db => db.Orders.ToListAsync());

        // Assert
        orderResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_order_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteOrder.DeleteOrderCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}