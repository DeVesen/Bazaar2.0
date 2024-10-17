using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class StatisticService
{
    private readonly HttpClient _httpClient;

    public StatisticService(IWebAssemblyHostEnvironment hostEnvironment, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/Statistic");
    }

    public async Task<Response<StatisticSummeryView>> Get()
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .Build();

            var domainElement =
                await _httpClient.GetFromJsonAsync<StatisticSummeryView>(requestUri);

            if (domainElement == null)
            {
                return Response<StatisticSummeryView>.Invalid("No data reseived!");
            }

            return Response<StatisticSummeryView>.Valid(domainElement);
        }
        catch (Exception ex)
        {
            return Response<StatisticSummeryView>.Invalid(ex.Message);
        }
    }
}