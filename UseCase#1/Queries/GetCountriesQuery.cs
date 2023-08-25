using MediatR;
using System.Text.Json;
using UseCase_1.Interfaces;
using UseCase_1.Models;

namespace UseCase_1.Queries;

public class GetCountriesQuery : IRequest<IReadOnlyCollection<Country>>
{
    public string CountryNameFilter { get; set; } = string.Empty;
    public int PopulationFilter { get; set; }
    public string OrderByNameOption { get; set; } = "Ascend";
    public int CountriesCount { get; set; }
}

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IReadOnlyCollection<Country>>
{
    private readonly ICountryFilterBuilder _countriesBuilder;
    private readonly HttpClient _httpClient;

    public GetCountriesQueryHandler(
        ICountryFilterBuilder countriesBuilder,
        HttpClient httpClient)
    {
        _countriesBuilder = countriesBuilder;
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<Country>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all", cancellationToken);

        var countries = JsonSerializer.Deserialize<Country[]>(
            response.Content.ReadAsStringAsync(cancellationToken).Result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? Array.Empty<Country>();

        var filteredCountries = _countriesBuilder
            .InitCountries(countries)
            .AddCountryNameFilter(request.CountryNameFilter)
            .AddPopulationFilter(request.PopulationFilter)
            .AddCountryNameOrder(request.OrderByNameOption)
            .Build()
            .Take(request.CountriesCount)
            .ToArray();

        return filteredCountries;
    }
}

