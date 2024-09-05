using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;

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

    public async Task<Response<bool>> ExistsAsync(string value)
    {
        var elementResult = await GetAllAsync(value);

        return elementResult.IsValid
            ? Response<bool>.Valid(elementResult.Value.Any())
            : Response<bool>.Invalid(elementResult.ErrorMessages);
    }

    public async Task<Response<IEnumerable<ArticleCategoryDto>>> GetAllAsync(string? name = null, string? searchText = null)
    {
        try
        {
            var queryBuilder = new ApiQueryBuilder
            {
                ["Name"] = name,
                ["SearchText"] = searchText
            };

            var dtoElements =
                await _httpClient.GetFromJsonAsync<IEnumerable<ArticleCategoryDto>>(queryBuilder.BuildFinal()) ?? [];

            return Response<IEnumerable<ArticleCategoryDto>>.Valid(dtoElements);
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<ArticleCategoryDto>>.Invalid(ex.Message);
        }
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
}