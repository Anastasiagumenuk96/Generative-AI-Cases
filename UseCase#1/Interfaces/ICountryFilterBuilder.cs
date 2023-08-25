using UseCase_1.Models;

namespace UseCase_1.Interfaces;

public interface ICountryFilterBuilder
{
    Country[] Build();
    ICountryFilterBuilder InitCountries(Country[] countries);
    ICountryFilterBuilder AddCountryNameFilter(string countryName);
    ICountryFilterBuilder AddPopulationFilter(int population);
}

