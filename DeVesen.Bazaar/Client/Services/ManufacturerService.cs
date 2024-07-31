using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class ManufacturerService
{
    private readonly HttpClient _httpClient;

    public ManufacturerService(IWebAssemblyHostEnvironment hostEnvironment,
        HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/Manufacturer");
    }

    public async Task<bool> ExistsAsync(string value)
    {
        var elements = await GetAllAsync();
        return elements.Any(p => p.Name.Equals(value, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Manufacturer>> GetAllAsync()
    {
        var dtoList = await _httpClient.GetFromJsonAsync<IEnumerable<ManufacturerDto>>("");

        return dtoList!.Select(p => new Manufacturer
        {
            Id = p.Id,
            Name = p.Name
        });
    }

    public async Task<Response> CreateAsync(Manufacturer element)
    {
        var requestUri = _httpClient.BaseAddress;
        var createDto = new ManufacturerCreateDto(element.Name);

        var response = await _httpClient.PostAsJsonAsync(requestUri, createDto);

        return response.IsSuccessStatusCode
            ? Response.Valid()
            : Response.Invalid();
    }

    public async Task<Response> UpdateAsync(Manufacturer element)
    {
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";
        var updateDto = new ManufacturerUpdateDto(element.Name);

        var response = await _httpClient.PutAsJsonAsync(requestUri, updateDto);

        return response.IsSuccessStatusCode
            ? Response.Valid()
            : Response.Invalid();
    }

    public async Task<Response> DeleteAsync(Manufacturer element)
    {
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";

        var response = await _httpClient.DeleteAsync(requestUri);

        return response.IsSuccessStatusCode
            ? Response.Valid()
            : Response.Invalid();
    }

    public bool FilterFunc(Manufacturer dto, string filterString)
    {
        if (string.IsNullOrWhiteSpace(filterString))
            return true;

        if (dto.Name.Contains(filterString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}