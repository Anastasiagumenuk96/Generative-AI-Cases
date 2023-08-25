using UseCase_1.Interfaces;
using UseCase_1.Models;
using static UseCase_1.Constants.OrderOptions;

namespace UseCase_1.Filters;

public class CountryFilterBuilder : ICountryFilterBuilder
{
    private Country[] _countries = Array.Empty<Country>();

    public Country[] Build() => _countries;

    public ICountryFilterBuilder InitCountries(Country[] countries)
    {
        _countries = countries;

        return this;
    }

    public ICountryFilterBuilder AddCountryNameFilter(string countryName)
    {
        if (!string.IsNullOrEmpty(countryName))
        {
            _countries = _countries
                .Where(x => x.Name.Common.Contains(countryName.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }

        return this;
    }

    public ICountryFilterBuilder AddPopulationFilter(int population)
    {
        if (population > 0)
        {
            var populationInMillions = population * 1000000;

            _countries = _countries
                .Where(x => x.Population < populationInMillions)
                .ToArray();
        }

        return this;
    }

    public ICountryFilterBuilder AddCountryNameOrder(string orderOption)
    {
        if (orderOption.Trim().Equals(Ascend, StringComparison.OrdinalIgnoreCase))
        {
            _countries = _countries
                .OrderBy(x => x.Name.Common)
                .ToArray();
        }
        else if (orderOption.Trim().Equals(Descend, StringComparison.OrdinalIgnoreCase))
        {
            _countries = _countries
                .OrderByDescending(x => x.Name.Common)
                .ToArray();
        }

        return this;
    }
}

