namespace Ordering.IntegrationTests.FeatureTests.OrderLine;

using Ordering.SharedTestHelpers.Fakes.OrderLine;
using Ordering.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Ordering.Domain.OrderLines.Features;
using static TestFixture;

public class DeleteOrderLineCommandTests : TestBase
{
    [Test]
    public async Task can_delete_orderline_from_db()
    {
        // Arrange
        var fakeOrderLineOne = new FakeOrderLine { }.Generate();
        await InsertAsync(fakeOrderLineOne);
        var orderLine = await ExecuteDbContextAsync(db => db.OrderLines.SingleOrDefaultAsync());
        var id = orderLine.Id;

        // Act
        var command = new DeleteOrderLine.DeleteOrderLineCommand(id);
        await SendAsync(command);
        var orderLineResponse = await ExecuteDbContextAsync(db => db.OrderLines.ToListAsync());

        // Assert
        orderLineResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_orderline_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteOrderLine.DeleteOrderLineCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}