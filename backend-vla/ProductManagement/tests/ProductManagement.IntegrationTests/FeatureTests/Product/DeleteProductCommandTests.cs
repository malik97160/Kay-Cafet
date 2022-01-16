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

public class DeleteProductCommandTests : TestBase
{
    [Test]
    public async Task can_delete_product_from_db()
    {
        // Arrange
        var fakeProductOne = new FakeProduct { }.Generate();
        await InsertAsync(fakeProductOne);
        var product = await ExecuteDbContextAsync(db => db.Products.SingleOrDefaultAsync());
        var id = product.Id;

        // Act
        var command = new DeleteProduct.DeleteProductCommand(id);
        await SendAsync(command);
        var productResponse = await ExecuteDbContextAsync(db => db.Products.ToListAsync());

        // Assert
        productResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_product_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteProduct.DeleteProductCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}