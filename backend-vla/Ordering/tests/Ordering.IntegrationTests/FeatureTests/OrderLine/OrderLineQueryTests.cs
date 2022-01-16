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

public class OrderLineQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_orderline_with_accurate_props()
    {
        // Arrange
        var fakeOrderLineOne = new FakeOrderLine { }.Generate();
        await InsertAsync(fakeOrderLineOne);

        // Act
        var query = new GetOrderLine.OrderLineQuery(fakeOrderLineOne.Id);
        var orderLines = await SendAsync(query);

        // Assert
        orderLines.Should().BeEquivalentTo(fakeOrderLineOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_orderline_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetOrderLine.OrderLineQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}