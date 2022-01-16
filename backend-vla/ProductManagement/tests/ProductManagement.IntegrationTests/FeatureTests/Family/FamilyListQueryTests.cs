namespace ProductManagement.IntegrationTests.FeatureTests.Family;

using ProductManagement.Dtos.Family;
using ProductManagement.SharedTestHelpers.Fakes.Family;
using ProductManagement.Exceptions;
using ProductManagement.Domain.Familys.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class FamilyListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_family_list()
    {
        // Arrange
        var fakeFamilyOne = new FakeFamily { }.Generate();
        var fakeFamilyTwo = new FakeFamily { }.Generate();
        var queryParameters = new FamilyParametersDto();

        await InsertAsync(fakeFamilyOne, fakeFamilyTwo);

        // Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var familys = await SendAsync(query);

        // Assert
        familys.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_family_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeFamilyOne = new FakeFamily { }.Generate();
        var fakeFamilyTwo = new FakeFamily { }.Generate();
        var fakeFamilyThree = new FakeFamily { }.Generate();
        var queryParameters = new FamilyParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeFamilyOne, fakeFamilyTwo, fakeFamilyThree);

        //Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var familys = await SendAsync(query);

        // Assert
        familys.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_family_by_Name_in_asc_order()
    {
        //Arrange
        var fakeFamilyOne = new FakeFamily { }.Generate();
        var fakeFamilyTwo = new FakeFamily { }.Generate();
        fakeFamilyOne.Name = "bravo";
        fakeFamilyTwo.Name = "alpha";
        var queryParameters = new FamilyParametersDto() { SortOrder = "Name" };

        await InsertAsync(fakeFamilyOne, fakeFamilyTwo);

        //Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var familys = await SendAsync(query);

        // Assert
        familys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFamilyTwo, options =>
                options.ExcludingMissingMembers());
        familys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFamilyOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_family_by_Name_in_desc_order()
    {
        //Arrange
        var fakeFamilyOne = new FakeFamily { }.Generate();
        var fakeFamilyTwo = new FakeFamily { }.Generate();
        fakeFamilyOne.Name = "alpha";
        fakeFamilyTwo.Name = "bravo";
        var queryParameters = new FamilyParametersDto() { SortOrder = "-Name" };

        await InsertAsync(fakeFamilyOne, fakeFamilyTwo);

        //Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var familys = await SendAsync(query);

        // Assert
        familys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFamilyTwo, options =>
                options.ExcludingMissingMembers());
        familys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFamilyOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_family_list_using_Name()
    {
        //Arrange
        var fakeFamilyOne = new FakeFamily { }.Generate();
        var fakeFamilyTwo = new FakeFamily { }.Generate();
        fakeFamilyOne.Name = "alpha";
        fakeFamilyTwo.Name = "bravo";
        var queryParameters = new FamilyParametersDto() { Filters = $"Name == {fakeFamilyTwo.Name}" };

        await InsertAsync(fakeFamilyOne, fakeFamilyTwo);

        //Act
        var query = new GetFamilyList.FamilyListQuery(queryParameters);
        var familys = await SendAsync(query);

        // Assert
        familys.Should().HaveCount(1);
        familys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeFamilyTwo, options =>
                options.ExcludingMissingMembers());
    }

}