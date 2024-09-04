using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class VendorService
{
    private readonly HttpClient _httpClient;
    private readonly SnackBarService _snackBarService;

    public VendorService(IWebAssemblyHostEnvironment hostEnvironment, HttpClient httpClient, SnackBarService snackBarService)
    {
        _httpClient = httpClient;
        _snackBarService = snackBarService;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/Vendor");
    }

    public async Task<VendorView?> GetAsync(string id)
    {
        var requestUri = new UriBuilder(_httpClient.BaseAddress)
                                .AddUriPart(id)
                                .Build();

        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        var result = await _httpClient.GetFromJsonAsync<VendorView?>(requestUri);

        return result;
    }

    public async Task<IEnumerable<VendorView>> GetAllAsync(string? id = null, string? salutation = null, string? searchText = null)
    {
        var requestUri = new UriBuilder(_httpClient.BaseAddress)
                                .SetQueryItem("Id", id)
                                .SetQueryItem("Salutation", salutation)
                                .SetQueryItem("SearchText", searchText)
                                .Build();

        return await _httpClient.GetFromJsonAsync<IEnumerable<VendorView>>(requestUri) ?? [];
    }

    public async Task<Response<Vendor>> CreateAsync(Vendor element)
    {
        var requestUri = new UriBuilder(_httpClient.BaseAddress)
                                .Build();
        var createDto = element.ToCreateDto();

        var response = await _httpClient.PostAsJsonAsync(requestUri, createDto);

        if (response.IsSuccessStatusCode)
        {
            var vendor = await response.Content.ReadFromJsonAsync<Vendor>();

            _snackBarService.AddInfo("Erfolgreich angelegt ...");
            return Response<Vendor>.Valid(vendor!);
        }

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}");
        return Response<Vendor>.Invalid();
    }

    public async Task<Response> UpdateAsync(Vendor element)
    {
        var requestUri = new UriBuilder(_httpClient.BaseAddress)
                                .AddUriPart(element.Id)
                                .Build();
        var updateDto = element.ToUpdateDto();

        var response = await _httpClient.PutAsJsonAsync(requestUri, updateDto);

        if (response.IsSuccessStatusCode)
        {
            _snackBarService.AddInfo("Erfolgreich aktualisiert ...");
            return Response.Valid();
        }

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}");
        return Response.Invalid();
    }

    public async Task<Response> DeleteAsync(Vendor element)
    {
        var requestUri = new UriBuilder(_httpClient.BaseAddress)
                                .AddUriPart(element.Id)
                                .Build();

        var response = await _httpClient.DeleteAsync(requestUri);

        if (response.IsSuccessStatusCode)
        {
            _snackBarService.AddInfo("Erfolgreich gelöscht ...");
            return Response.Valid();
        }

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}");
        return Response.Invalid();
    }
}