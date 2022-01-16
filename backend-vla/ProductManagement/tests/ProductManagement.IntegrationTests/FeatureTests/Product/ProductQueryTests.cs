namespace ProductManagement.IntegrationTests.FeatureTests.Product;

using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ProductManagement.Domain.Products.Features;
using static TestFixture;

public class ProductQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_product_with_accurate_props()
    {
        // Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        await InsertAsync(fakeProductOne);

        // Act
        var query = new GetProduct.ProductQuery(fakeProductOne.Id);
        var products = await SendAsync(query);

        // Assert
        products.Should().BeEquivalentTo(fakeProductOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_product_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetProduct.ProductQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}