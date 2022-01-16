namespace ProductManagement.IntegrationTests.FeatureTests.Product;

using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ProductManagement.Domain.Products.Features;
using static TestFixture;
using ProductManagement.Exceptions;

public class AddProductCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_product_to_db()
    {
        // Arrange
        var fakeProductOne = new FakeProductForCreationDto { }.Generate();

        // Act
        var command = new AddProduct.AddProductCommand(fakeProductOne);
        var productReturned = await SendAsync(command);
        var productCreated = await ExecuteDbContextAsync(db => db.Products.SingleOrDefaultAsync());

        // Assert
        productReturned.Should().BeEquivalentTo(fakeProductOne, options =>
            options.ExcludingMissingMembers());
        productCreated.Should().BeEquivalentTo(fakeProductOne, options =>
            options.ExcludingMissingMembers());
    }
}