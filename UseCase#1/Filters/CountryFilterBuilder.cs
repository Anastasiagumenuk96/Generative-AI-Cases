using UseCase_1.Interfaces;
using UseCase_1.Models;

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
}

