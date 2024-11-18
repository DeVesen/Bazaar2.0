using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Statistics;
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

    public async Task<Response<VendorOverviewItem>> GetByIdAsync(string id)
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .AddUriPart(id)
                .Build();

            if (string.IsNullOrWhiteSpace(id))
            {
                return Response<VendorOverviewItem>.Invalid("Verkäufer nicht gefunden!");
            }

            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode is false)
            {
                return Response<VendorOverviewItem>.Invalid("Verkäufer nicht gefunden!");
            }

            var domainElement = await response.Content.ReadFromJsonAsync<VendorOverviewItem>();

            return Response<VendorOverviewItem>.Valid(domainElement!);
        }
        catch (Exception ex)
        {
            return Response<VendorOverviewItem>.Invalid(ex.Message);
        }
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



    public async Task<Response<IEnumerable<VendorOverviewItem>>> GetOverviewAsync(string? id = null, string? searchText = null)
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .AddUriPart("overview")
                .SetQueryItem("Id", id)
                .SetQueryItem("SearchText", searchText)
                .Build();

            var elements =
                await _httpClient.GetFromJsonAsync<IEnumerable<VendorOverviewItem>>(requestUri) ?? [];

            return Response<IEnumerable<VendorOverviewItem>>.Valid(elements.ToArray());
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<VendorOverviewItem>>.Invalid(ex.Message);
        }
    }

    public async Task<Response<(CountsStatistics Counts, ValuesStatistics Values)>> GetStatisticsByVendor(string vendorId)
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .AddUriPart(vendorId)
                .AddUriPart("statistics")
                .Build();

            var response =
                await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode is false)
            {
                return Response<(CountsStatistics Counts, ValuesStatistics Values)>.Invalid($"Verkäufer Statistik für '{vendorId}' konnte nicht geladen werden: {response.StatusCode}");
            }

            var statistics = await response.Content.ReadFromJsonAsync<VendorStatisticsDto>();

            return Response<(CountsStatistics Counts, ValuesStatistics Values)>.Valid((statistics!.Counts, statistics.Values));
        }
        catch (Exception ex)
        {
            return Response<(CountsStatistics Counts, ValuesStatistics Values)>.Invalid(ex.Message);
        }
    }
}