namespace Ordering.IntegrationTests.FeatureTests.Order;

using Ordering.Dtos.Order;
using Ordering.SharedTestHelpers.Fakes.Order;
using Ordering.Exceptions;
using Ordering.Domain.Orders.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class OrderListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_order_list()
    {
        // Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        var queryParameters = new OrderParametersDto();

        await InsertAsync(fakeOrderOne, fakeOrderTwo);

        // Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_order_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        var fakeOrderThree = new FakeOrder { }.Generate();
        var queryParameters = new OrderParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeOrderOne, fakeOrderTwo, fakeOrderThree);

        //Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_order_by_ExpectedPickupTime_in_asc_order()
    {
        //Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        fakeOrderOne.ExpectedPickupTime = DateTime.Now.AddDays(2);
        fakeOrderTwo.ExpectedPickupTime = DateTime.Now.AddDays(1);
        var queryParameters = new OrderParametersDto() { SortOrder = "ExpectedPickupTime" };

        await InsertAsync(fakeOrderOne, fakeOrderTwo);

        //Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderTwo, options =>
                options.ExcludingMissingMembers());
        orders
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_order_by_ExpectedPickupTime_in_desc_order()
    {
        //Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        fakeOrderOne.ExpectedPickupTime = DateTime.Now.AddDays(1);
        fakeOrderTwo.ExpectedPickupTime = DateTime.Now.AddDays(2);
        var queryParameters = new OrderParametersDto() { SortOrder = "-ExpectedPickupTime" };

        await InsertAsync(fakeOrderOne, fakeOrderTwo);

        //Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderTwo, options =>
                options.ExcludingMissingMembers());
        orders
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_order_by_Status_in_asc_order()
    {
        //Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        fakeOrderOne.Status = 2;
        fakeOrderTwo.Status = 1;
        var queryParameters = new OrderParametersDto() { SortOrder = "Status" };

        await InsertAsync(fakeOrderOne, fakeOrderTwo);

        //Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderTwo, options =>
                options.ExcludingMissingMembers());
        orders
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_order_by_Status_in_desc_order()
    {
        //Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        fakeOrderOne.Status = 1;
        fakeOrderTwo.Status = 2;
        var queryParameters = new OrderParametersDto() { SortOrder = "-Status" };

        await InsertAsync(fakeOrderOne, fakeOrderTwo);

        //Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderTwo, options =>
                options.ExcludingMissingMembers());
        orders
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_order_list_using_CustomerId()
    {
        //Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        fakeOrderOne.CustomerId = Guid.NewGuid();
        fakeOrderTwo.CustomerId = Guid.NewGuid();
        var queryParameters = new OrderParametersDto() { Filters = $"CustomerId == {fakeOrderTwo.CustomerId}" };

        await InsertAsync(fakeOrderOne, fakeOrderTwo);

        //Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders.Should().HaveCount(1);
        orders
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_order_list_using_ExpectedPickupTime()
    {
        //Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        fakeOrderOne.ExpectedPickupTime = DateTime.Now.AddDays(1);
        fakeOrderTwo.ExpectedPickupTime = DateTime.Parse(DateTime.Now.AddDays(2).ToString("MM/dd/yyyy"));
        var queryParameters = new OrderParametersDto() { Filters = $"ExpectedPickupTime == {fakeOrderTwo.ExpectedPickupTime}" };

        await InsertAsync(fakeOrderOne, fakeOrderTwo);

        //Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders.Should().HaveCount(1);
        orders
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_order_list_using_Status()
    {
        //Arrange
        var fakeOrderOne = new FakeOrder { }.Generate();
        var fakeOrderTwo = new FakeOrder { }.Generate();
        fakeOrderOne.Status = 1;
        fakeOrderTwo.Status = 2;
        var queryParameters = new OrderParametersDto() { Filters = $"Status == {fakeOrderTwo.Status}" };

        await InsertAsync(fakeOrderOne, fakeOrderTwo);

        //Act
        var query = new GetOrderList.OrderListQuery(queryParameters);
        var orders = await SendAsync(query);

        // Assert
        orders.Should().HaveCount(1);
        orders
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeOrderTwo, options =>
                options.ExcludingMissingMembers());
    }

}