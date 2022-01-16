namespace ProductManagement.IntegrationTests.FeatureTests.Product;

using ProductManagement.SharedTestHelpers.Fakes.Product;
using ProductManagement.IntegrationTests.TestUtilities;
using ProductManagement.Dtos.Product;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ProductManagement.Domain.Products.Features;
using static TestFixture;

public class UpdateProductCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_product_in_db()
    {
        // Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        var updatedProductDto = new FakeProductForUpdateDto { }.Generate();
        await InsertAsync(fakeProductOne);

        var product = await ExecuteDbContextAsync(db => db.Products.SingleOrDefaultAsync());
        var id = product.Id;

        // Act
        var command = new UpdateProduct.UpdateProductCommand(id, updatedProductDto);
        await SendAsync(command);
        var updatedProduct = await ExecuteDbContextAsync(db => db.Products.Where(p => p.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedProduct.Should().BeEquivalentTo(updatedProductDto, options =>
            options.ExcludingMissingMembers());
    }
}