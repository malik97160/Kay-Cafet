namespace ProductManagement.IntegrationTests.FeatureTests.Ingredient;

using ProductManagement.Dtos.Ingredient;
using ProductManagement.SharedTestHelpers.Fakes.Ingredient;
using ProductManagement.Exceptions;
using ProductManagement.Domain.Ingredients.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class IngredientListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_ingredient_list()
    {
        // Arrange
        var fakeIngredientOne = new FakeIngredient { }.Generate();
        var fakeIngredientTwo = new FakeIngredient { }.Generate();
        var queryParameters = new IngredientParametersDto();

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo);

        // Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_ingredient_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeIngredientOne = new FakeIngredient { }.Generate();
        var fakeIngredientTwo = new FakeIngredient { }.Generate();
        var fakeIngredientThree = new FakeIngredient { }.Generate();
        var queryParameters = new IngredientParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo, fakeIngredientThree);

        //Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_ingredient_by_Unit_in_asc_order()
    {
        //Arrange
        var fakeIngredientOne = new FakeIngredient { }.Generate();
        var fakeIngredientTwo = new FakeIngredient { }.Generate();
        fakeIngredientOne.Unit = "bravo";
        fakeIngredientTwo.Unit = "alpha";
        var queryParameters = new IngredientParametersDto() { SortOrder = "Unit" };

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo);

        //Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIngredientTwo, options =>
                options.ExcludingMissingMembers());
        ingredients
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIngredientOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_ingredient_by_Unit_in_desc_order()
    {
        //Arrange
        var fakeIngredientOne = new FakeIngredient { }.Generate();
        var fakeIngredientTwo = new FakeIngredient { }.Generate();
        fakeIngredientOne.Unit = "alpha";
        fakeIngredientTwo.Unit = "bravo";
        var queryParameters = new IngredientParametersDto() { SortOrder = "-Unit" };

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo);

        //Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIngredientTwo, options =>
                options.ExcludingMissingMembers());
        ingredients
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIngredientOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_ingredient_list_using_Unit()
    {
        //Arrange
        var fakeIngredientOne = new FakeIngredient { }.Generate();
        var fakeIngredientTwo = new FakeIngredient { }.Generate();
        fakeIngredientOne.Unit = "alpha";
        fakeIngredientTwo.Unit = "bravo";
        var queryParameters = new IngredientParametersDto() { Filters = $"Unit == {fakeIngredientTwo.Unit}" };

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo);

        //Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().HaveCount(1);
        ingredients
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIngredientTwo, options =>
                options.ExcludingMissingMembers());
    }

}