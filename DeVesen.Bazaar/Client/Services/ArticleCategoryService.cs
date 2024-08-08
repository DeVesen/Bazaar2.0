using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.Services;

public class ArticleCategoryService
{
    private readonly HttpClient _httpClient;
    private readonly SnackBarService _snackBarService;

    public ArticleCategoryService(IWebAssemblyHostEnvironment hostEnvironment,
                                  HttpClient httpClient,
                                  SnackBarService snackBarService)
    {
        _httpClient = httpClient;
        _snackBarService = snackBarService;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/ArticleCategory");
    }

    public async Task<bool> ExistsAsync(string value)
    {
        var elements = await GetAllAsync();
        return elements.Any(p => p.Name.Equals(value, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<ArticleCategory>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ArticleCategory>>("") ?? Enumerable.Empty<ArticleCategory>();
    }

    public async Task<Response> CreateAsync(ArticleCategory element)
    {
        var requestUri = _httpClient.BaseAddress;
        var createDto = element.ToCreateDto();

        var response = await _httpClient.PostAsJsonAsync(requestUri, createDto);

        if (response.IsSuccessStatusCode)
        {
            _snackBarService.AddInfo("Erfolgreich angelegt ...");
            return Response.Valid();
        }

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}");
        return Response.Invalid();
    }

    public async Task<Response> UpdateAsync(ArticleCategory element)
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

    public async Task<Response> DeleteAsync(ArticleCategory element)
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

    public bool FilterFunc(ArticleCategory dto, string filterString)
    {
        return string.IsNullOrWhiteSpace(filterString)
            || dto.Name.Contains(filterString, StringComparison.OrdinalIgnoreCase);
    }
}