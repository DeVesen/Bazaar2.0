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

    public VendorService(IWebAssemblyHostEnvironment hostEnvironment,
        HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/Vendor");
    }

    public async Task<IEnumerable<Vendor>> GetAllAsync()
    {
        var dtoList = await _httpClient.GetFromJsonAsync<IEnumerable<VendorDto>>("");

        return dtoList!.Select(p => new Vendor
        {
            Id = p.Id,
            Salutation = p.Salutation,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Address = p.Address,
            EMail = p.EMail,
            Phone = p.Phone
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

        return response.IsSuccessStatusCode
            ? Response.Valid()
            : Response.Invalid();
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

        return response.IsSuccessStatusCode
            ? Response.Valid()
            : Response.Invalid();
    }

    public async Task<Response> DeleteAsync(Vendor element)
    {
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";

        var response = await _httpClient.DeleteAsync(requestUri);

        return response.IsSuccessStatusCode
            ? Response.Valid()
            : Response.Invalid();
    }
}