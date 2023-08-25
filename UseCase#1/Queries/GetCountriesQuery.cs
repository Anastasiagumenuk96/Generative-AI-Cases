using MediatR;
using System.Text.Json;
using UseCase_1.Models;

namespace UseCase_1.Queries;

public class GetCountriesQuery : IRequest<IReadOnlyCollection<Country>>
{
    public string CountryNameFilter { get; set; } = string.Empty;
}

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IReadOnlyCollection<Country>>
{
    private readonly HttpClient _httpClient;

    public GetCountriesQueryHandler(HttpClient httpClient)
    {
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

        if (string.IsNullOrEmpty(request.CountryNameFilter))
        {
            return countries;
        }

        var filteredCountries = countries
            .Where(x => x.Name.Common.Contains(request.CountryNameFilter, StringComparison.OrdinalIgnoreCase))
            .ToArray();

        return filteredCountries;
    }
}

