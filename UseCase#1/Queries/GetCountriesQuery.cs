using MediatR;
using System.Text.Json;
using UseCase_1.Interfaces;
using UseCase_1.Models;
using static UseCase_1.Constants.OrderOptions;

namespace UseCase_1.Queries;

public class GetCountriesQuery : IRequest<IReadOnlyCollection<Country>>
{
    public string CountryNameFilter { get; set; } = string.Empty;
    public int PopulationFilter { get; set; }
    public string OrderByNameOption { get; set; } = "Ascend";
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
            .Build();

        if (request.OrderByNameOption.Trim().Equals(Ascend, StringComparison.OrdinalIgnoreCase))
        {
            filteredCountries = filteredCountries
                .OrderBy(x => x.Name.Common)
                .ToArray();
        }
        else if (request.OrderByNameOption.Trim().Equals(Descend, StringComparison.OrdinalIgnoreCase))
        {
            filteredCountries = filteredCountries
                .OrderByDescending(x => x.Name.Common)
                .ToArray();
        }

        return filteredCountries;
    }
}

