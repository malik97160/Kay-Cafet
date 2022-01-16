namespace ProductManagement.IntegrationTests.FeatureTests.Product;

using ProductManagement.Dtos.Product;
using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.Exceptions;
using ProductManagement.Domain.Products.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class ProductListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_product_list()
    {
        // Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        var queryParameters = new ProductParametersDto();

        await InsertAsync(fakeProductOne, fakeProductTwo);

        // Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_product_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        var fakeProductThree = new FakeProduct { }.Generate();
        var queryParameters = new ProductParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeProductOne, fakeProductTwo, fakeProductThree);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_product_by_Name_in_asc_order()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.Name = "bravo";
        fakeProductTwo.Name = "alpha";
        var queryParameters = new ProductParametersDto() { SortOrder = "Name" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
        products
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_product_by_Name_in_desc_order()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.Name = "alpha";
        fakeProductTwo.Name = "bravo";
        var queryParameters = new ProductParametersDto() { SortOrder = "-Name" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
        products
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_product_by_Description_in_asc_order()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.Description = "bravo";
        fakeProductTwo.Description = "alpha";
        var queryParameters = new ProductParametersDto() { SortOrder = "Description" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
        products
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_product_by_Description_in_desc_order()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.Description = "alpha";
        fakeProductTwo.Description = "bravo";
        var queryParameters = new ProductParametersDto() { SortOrder = "-Description" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
        products
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_product_by_ImageLink_in_asc_order()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.ImageLink = "bravo";
        fakeProductTwo.ImageLink = "alpha";
        var queryParameters = new ProductParametersDto() { SortOrder = "ImageLink" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
        products
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_product_by_ImageLink_in_desc_order()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.ImageLink = "alpha";
        fakeProductTwo.ImageLink = "bravo";
        var queryParameters = new ProductParametersDto() { SortOrder = "-ImageLink" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
        products
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_product_list_using_Name()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.Name = "alpha";
        fakeProductTwo.Name = "bravo";
        var queryParameters = new ProductParametersDto() { Filters = $"Name == {fakeProductTwo.Name}" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products.Should().HaveCount(1);
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_product_list_using_Description()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.Description = "alpha";
        fakeProductTwo.Description = "bravo";
        var queryParameters = new ProductParametersDto() { Filters = $"Description == {fakeProductTwo.Description}" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products.Should().HaveCount(1);
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_product_list_using_ImageLink()
    {
        //Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var fakeProductTwo = new FakeProduct { }.Generate();
        fakeProductOne.ImageLink = "alpha";
        fakeProductTwo.ImageLink = "bravo";
        var queryParameters = new ProductParametersDto() { Filters = $"ImageLink == {fakeProductTwo.ImageLink}" };

        await InsertAsync(fakeProductOne, fakeProductTwo);

        //Act
        var query = new GetProductList.ProductListQuery(queryParameters);
        var products = await SendAsync(query);

        // Assert
        products.Should().HaveCount(1);
        products
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeProductTwo, options =>
                options.ExcludingMissingMembers());
    }

}