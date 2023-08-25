using UseCase_1.Filters;
using UseCase_1.Models;
using Xunit;

namespace UseCase_1.Tests;

public class CountryFilterBuilderTests
{
    private readonly Country[] _countries;
    private readonly CountryFilterBuilder _countryFilterBuilder;

    public CountryFilterBuilderTests()
    {
        _countries = new Country[]
        {
            new()
            {
                Name = new()
                {
                    Common = "Ukraine"
                },
                Population = 44000000
            },
            new()
            {
                Name = new()
                {
                    Common = "Poland"
                },
                Population = 38000000
            },
            new()
            {
                Name = new()
                {
                    Common = "France"
                },
                Population = 68000000
            }
        };

        _countryFilterBuilder = new CountryFilterBuilder();
    }

    [Fact]
    public void AddCountryNameFilter_FilteredCountriesWereReturned()
    {
        //Arrange

        //Act
        var countries = _countryFilterBuilder
            .InitCountries(_countries)
            .AddCountryNameFilter("Uk")
            .Build();

        //Assert
        Assert.NotEmpty(countries);
        Assert.True(countries.Length == 1);
        Assert.Equal("Ukraine", countries.First().Name.Common);
    }

    [Fact]
    public void AddPopulationFilter_FilteredCountriesWereReturned()
    {
        //Arrange

        //Act
        var countries = _countryFilterBuilder
            .InitCountries(_countries)
            .AddPopulationFilter(44)
            .Build();

        //Assert
        Assert.NotEmpty(countries);
        Assert.True(countries.Length == 1);
        Assert.Equal("Poland", countries.First().Name.Common);
    }

    [Fact]
    public void AddCountryNameOrder_AscOrder_OrderedCountriesWereReturned()
    {
        //Arrange

        //Act
        var countries = _countryFilterBuilder
            .InitCountries(_countries)
            .AddCountryNameOrder("ascend")
            .Build();

        //Assert
        Assert.NotEmpty(countries);
        Assert.True(countries.Length == 3);
        Assert.Equal("France", countries.First().Name.Common);
        Assert.Equal("Ukraine", countries.Last().Name.Common);
    }

    [Fact]
    public void AddCountryNameOrder_DescOrder_OrderedCountriesWereReturned()
    {
        //Arrange

        //Act
        var countries = _countryFilterBuilder
            .InitCountries(_countries)
            .AddCountryNameOrder("descend")
            .Build();

        //Assert
        Assert.NotEmpty(countries);
        Assert.True(countries.Length == 3);
        Assert.Equal("Ukraine", countries.First().Name.Common);
        Assert.Equal("France", countries.Last().Name.Common);
    }

    [Fact]
    public void AddPagination_OrderedCountriesWereReturned()
    {
        //Arrange

        //Act
        var countries = _countryFilterBuilder
            .InitCountries(_countries)
            .AddPagination(1)
            .Build();

        //Assert
        Assert.NotEmpty(countries);
        Assert.True(countries.Length == 1);
    }

    [Fact]
    public void NullFilterValues_OriginalCountriesWereReturned()
    {
        //Arrange

        //Act
        var countries = _countryFilterBuilder
            .InitCountries(_countries)
            .AddCountryNameFilter(null)
            .AddPopulationFilter(null)
            .AddCountryNameOrder(null)
            .AddPagination(null)
            .Build();

        //Assert
        Assert.NotEmpty(countries);
        Assert.True(countries.Length == 3);
    }
}

