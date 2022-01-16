namespace ProductManagement.IntegrationTests.FeatureTests.Ingredient;

using ProductManagement.SharedTestHelpers.Fakes.Ingredient;
using ProductManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ProductManagement.Domain.Ingredients.Features;
using static TestFixture;

public class IngredientQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_ingredient_with_accurate_props()
    {
        // Arrange
        var fakeIngredientOne = new FakeIngredient { }.Generate();
        await InsertAsync(fakeIngredientOne);

        // Act
        var query = new GetIngredient.IngredientQuery(fakeIngredientOne.Id);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().BeEquivalentTo(fakeIngredientOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_ingredient_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetIngredient.IngredientQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}