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

    public async Task<Vendor?> GetAsync(string id)
    {
        return (await GetAllAsync(id)).FirstOrDefault()?.Item;
    }

    public async Task<IEnumerable<VendorView>> GetAllAsync(string? id = null, string? salutation = null, string? searchText = null)
    {
        var queryBuilder = new ApiQueryBuilder
        {
            ["Id"] = id,
            ["Salutation"] = salutation,
            ["SearchText"] = searchText
        };

        return await _httpClient.GetFromJsonAsync<IEnumerable<VendorView>>(queryBuilder.BuildFinal()) ?? Enumerable.Empty<VendorView>();
    }

    public async Task<Response<Vendor>> CreateAsync(Vendor element)
    {
        var requestUri = _httpClient.BaseAddress;
        var createDto = element.ToCreateDto();

        var response = await _httpClient.PostAsJsonAsync(requestUri, createDto);

        if (response.IsSuccessStatusCode)
        {
            var vendor = await response.Content.ReadFromJsonAsync<Vendor>();

            _snackBarService.AddInfo("Erfolgreich angelegt ...");
            return Response<Vendor>.Valid(vendor);
        }

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}");
        return Response<Vendor>.Invalid();
    }

    public async Task<Response> UpdateAsync(Vendor element)
    {
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";
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
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";

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