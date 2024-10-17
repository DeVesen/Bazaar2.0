using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class SettlementService
{
    private readonly HttpClient _httpClient;

    public SettlementService(IWebAssemblyHostEnvironment hostEnvironment, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/Settlement");
    }

    public async Task<Response<VendorSettlementItem>> GetAsync(string vendorId)
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .AddUriPart(vendorId)
                .Build();

            var element =
                await _httpClient.GetFromJsonAsync<VendorSettlementItem>(requestUri);

            return Response<VendorSettlementItem>.Valid(element!);
        }
        catch (Exception ex)
        {
            return Response<VendorSettlementItem>.Invalid(ex.Message);
        }
    }

    public async Task<Response> GiveBackArticleAsync(string vendorId, long articleNumber)
    {
        var requestUri = _httpClient.BaseAddress + $"/{vendorId}/giveback/{articleNumber}";
        var response = await _httpClient.PostAsync(requestUri, null);

        if (response.IsSuccessStatusCode)
        {
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        return Response.Invalid(message!.Message);
    }

    public async Task<Response> SetAsSettledAsync(string vendorId)
    {
        var requestUri = _httpClient.BaseAddress + $"/{vendorId}/settle";
        var response = await _httpClient.PutAsync(requestUri, null);

        if (response.IsSuccessStatusCode)
        {
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        return Response.Invalid(message!.Message);
    }
}