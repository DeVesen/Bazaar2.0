using DeVesen.Bazaar.Client.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class StatisticService(IWebAssemblyHostEnvironment hostEnvironment,
                              HttpClient httpClient,
                              SnackBarService snackBarService) : BaseService(hostEnvironment, httpClient, "api/v1/Statistic")
{
    public async Task<StatisticSummeryView?> GetAsync()
    {
        var requestUri = new UriBuilder(BaseAddress)
            .Build();

        var response = await GetFromJsonAsync<StatisticSummeryView>(requestUri);

        if (response.IsSuccess)
        {
            snackBarService.AddError("Statistic konnte nicht geladen werden!");
            Console.WriteLine($"Statistic Error Code: {response.StatusCode}");
            Console.WriteLine($"Statistic Error Msg: {response.ResponseData}");
        }

        return response.Response;
    }
}