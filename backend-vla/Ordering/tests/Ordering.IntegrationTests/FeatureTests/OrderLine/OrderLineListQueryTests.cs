namespace Ordering.IntegrationTests.FeatureTests.OrderLine;

using Ordering.Dtos.OrderLine;
using Ordering.SharedTestHelpers.Fakes.OrderLine;
using Ordering.Exceptions;
using Ordering.Domain.OrderLines.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class OrderLineListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_orderline_list()
    {
        // Arrange
        var fakeOrderLineOne = new FakeOrderLine { }.Generate();
        var fakeOrderLineTwo = new FakeOrderLine { }.Generate();
        var queryParameters = new OrderLineParametersDto();

        await InsertAsync(fakeOrderLineOne, fakeOrderLineTwo);

        // Act
        var query = new GetOrderLineList.OrderLineListQuery(queryParameters);
        var orderLines = await SendAsync(query);

        // Assert
        orderLines.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_orderline_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeOrderLineOne = new FakeOrderLine { }.Generate();
        var fakeOrderLineTwo = new FakeOrderLine { }.Generate();
        var fakeOrderLineThree = new FakeOrderLine { }.Generate();
        var queryParameters = new OrderLineParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeOrderLineOne, fakeOrderLineTwo, fakeOrderLineThree);

        //Act
        var query = new GetOrderLineList.OrderLineListQuery(queryParameters);
        var orderLines = await SendAsync(query);

        // Assert
        orderLines.Should().HaveCount(1);
    }
    
    
}