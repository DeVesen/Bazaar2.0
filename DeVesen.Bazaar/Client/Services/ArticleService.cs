using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class ArticleService
{
    private readonly HttpClient _httpClient;
    private readonly SnackBarService _snackBarService;

    public ArticleService(IWebAssemblyHostEnvironment hostEnvironment,
        HttpClient httpClient, SnackBarService snackBarService)
    {
        _httpClient = httpClient;
        _snackBarService = snackBarService;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/Article");
    }

    public async Task<IEnumerable<Article>> GetAllAsync(string? vendorId = null, string? number = null, string? searchText = null)
    {
        var queryBuilder = new ApiQueryBuilder
        {
            ["VendorId"] = vendorId,
            ["Number"] = $"{number}",
            ["SearchText"] = searchText
        };

        return await _httpClient.GetFromJsonAsync<IEnumerable<Article>>(queryBuilder.BuildFinal()) ?? Enumerable.Empty<Article>();
    }

    public async Task<Response> CreateAsync(Article element)
    {
        var requestUri = _httpClient.BaseAddress;
        var createDto = element.ToCreateDto();

        var response = await _httpClient.PostAsJsonAsync(requestUri, createDto);

        if (response.IsSuccessStatusCode)
        {
            _snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Title}' erfolgreich angelegt ...");
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}{Environment.NewLine}{message!.Message}");
        return Response.Invalid();
    }

    public async Task<Response> UpdateAsync(Article element)
    {
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";
        var updateDto = element.ToUpdateDto();

        var response = await _httpClient.PutAsJsonAsync(requestUri, updateDto);

        if (response.IsSuccessStatusCode)
        {
            _snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Title}' erfolgreich geändert ...");
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}{Environment.NewLine}{message!.Message}");
        return Response.Invalid();
    }

    public async Task<Response> DeleteAsync(Article element)
    {
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";

        var response = await _httpClient.DeleteAsync(requestUri);

        if (response.IsSuccessStatusCode)
        {
            _snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Title}' erfolgreich gelöscht ...");
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}{Environment.NewLine}{message!.Message}");
        return Response.Invalid();
    }

    public async Task<Response> ApproveAsync(long number)
    {
        var requestUri = _httpClient.BaseAddress + $"/{number}/approve";
        var response = await _httpClient.PostAsync(requestUri, null);

        if (response.IsSuccessStatusCode)
        {
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        return Response.Invalid(message!.Message);
    }
}