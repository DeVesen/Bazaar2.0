using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
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

    public async Task<IEnumerable<VendorView>> GetAllAsync()
    {
        var dtoList = await _httpClient.GetFromJsonAsync<IEnumerable<VendorViewDto>>("");

        return dtoList!.Select(p => new VendorView
        {
            Item = new Vendor
            {
                Id = p.Item.Id,
                Salutation = p.Item.Salutation,
                FirstName = p.Item.FirstName,
                LastName = p.Item.LastName,
                Address = p.Item.Address,
                EMail = p.Item.EMail,
                Phone = p.Item.Phone
            },
            Statistic = new VendorArticleStatistic
            {
                Open = p.Statistic.Open,
                Sold = p.Statistic.Sold,
                Settled = p.Statistic.Settled,
                Turnover = p.Statistic.Turnover,
            }
        });
    }

    public async Task<Response> CreateAsync(Vendor element)
    {
        var requestUri = _httpClient.BaseAddress;
        var createDto = new VendorCreateDto
        {
            Salutation = element.Salutation,
            FirstName = element.FirstName,
            LastName = element.LastName,
            Address = element.Address,
            EMail = element.EMail,
            Phone = element.Phone,
            Note = element.Note
        };

        var response = await _httpClient.PostAsJsonAsync(requestUri, createDto);

        if (response.IsSuccessStatusCode)
        {
            _snackBarService.AddInfo("Erfolgreich angelegt ...");
            return Response.Valid();
        }

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}");
        return Response.Invalid();
    }

    public async Task<Response> UpdateAsync(Vendor element)
    {
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";
        var updateDto = new VendorUpdateDto
        {
            Salutation = element.Salutation,
            FirstName = element.FirstName,
            LastName = element.LastName,
            Address = element.Address,
            EMail = element.EMail,
            Phone = element.Phone,
            Note = element.Note
        };

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