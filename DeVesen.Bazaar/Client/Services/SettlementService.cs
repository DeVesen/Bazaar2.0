using DeVesen.Bazaar.Client.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class SettlementService(IWebAssemblyHostEnvironment hostEnvironment,
                               HttpClient httpClient,
                               SnackBarService snackBarService) : BaseService(hostEnvironment, httpClient, "api/v1/Settlement")
{
    public async Task<(bool IsSuccess, VendorSettlementItem Data)> GetAsync(string vendorId)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(vendorId)
            .Build();

        var response = await GetFromJsonAsync<VendorSettlementItem>(requestUri);

        if (response.IsSuccess)
        {
            snackBarService.AddError($"Verkäufer {vendorId} konnte nicht geladen werden!");
            Console.WriteLine($"Settlement-Get {vendorId} Error Code: {response.StatusCode}");
            Console.WriteLine($"Settlement-Get {vendorId} Error Msg: {response.ResponseData}");
        }

        return (response.IsSuccess, response.Response);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> GiveBackArticleAsync(string vendorId, long articleNumber)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(vendorId)
            .AddUriPart("giveback")
            .AddUriPart(articleNumber)
            .Build();

        var response = await PostAsync(requestUri, null);

        if (response.IsSuccess)
        {
            return (true, string.Empty);
        }

        snackBarService.AddError($"Rückgabe von {articleNumber} an {vendorId} hat nicht geklappt!");
        Console.WriteLine($"Settle {articleNumber} to {vendorId} Error Code: {response.StatusCode}");
        Console.WriteLine($"Settle {articleNumber} to {vendorId} Error Msg: {response.ResponseData}");

        return (false, response.ResponseData);
    }

    public async Task SetVendorAsSettledAsync(string vendorId)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(vendorId)
            .AddUriPart("settle")
            .Build();

        var response = await PutAsync(requestUri, null);

        if (response.IsSuccess is false)
        {
            snackBarService.AddError($"Abrechnen von {vendorId} hat nicht geklappt!");
            Console.WriteLine($"Settle {vendorId} Error Code: {response.StatusCode}");
            Console.WriteLine($"Settle {vendorId} Error Msg: {response.ResponseData}");
        }
    }
}