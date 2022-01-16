namespace Ordering.IntegrationTests.FeatureTests.OrderLine;

using Ordering.SharedTestHelpers.Fakes.OrderLine;
using Ordering.IntegrationTests.TestUtilities;
using Ordering.Dtos.OrderLine;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Ordering.Domain.OrderLines.Features;
using static TestFixture;

public class UpdateOrderLineCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_orderline_in_db()
    {
        // Arrange
        var fakeOrderLineOne = new FakeOrderLine { }.Generate();
        var updatedOrderLineDto = new FakeOrderLineForUpdateDto { }.Generate();
        await InsertAsync(fakeOrderLineOne);

        var orderLine = await ExecuteDbContextAsync(db => db.OrderLines.SingleOrDefaultAsync());
        var id = orderLine.Id;

        // Act
        var command = new UpdateOrderLine.UpdateOrderLineCommand(id, updatedOrderLineDto);
        await SendAsync(command);
        var updatedOrderLine = await ExecuteDbContextAsync(db => db.OrderLines.Where(o => o.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedOrderLine.Should().BeEquivalentTo(updatedOrderLineDto, options =>
            options.ExcludingMissingMembers());
    }
}