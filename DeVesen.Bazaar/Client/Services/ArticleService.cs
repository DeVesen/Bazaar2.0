﻿using System.Net.Http.Json;
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

        var dtoList = await _httpClient.GetFromJsonAsync<IEnumerable<ArticleDto>>(queryBuilder.BuildFinal());

        return dtoList!.Select(data => new Article
        {
            Id = data.Id,
            VendorId = data.VendorId,
            Number = data.Number,
            Title = data.Title,
            ArticleCategory = data.ArticleCategory,
            Manufacturer = data.Manufacturer,
            Created = data.Created,
            ApprovedForSale = data.ApprovedForSale,
            Sold = data.Sold,
            SoldAt = data.SoldAt,
            Settled = data.Settled,
            Price01 = data.Price01,
            Price02 = data.Price02,
            Description = data.Description
        });
    }

    public async Task<Response> CreateAsync(Article element)
    {
        var requestUri = _httpClient.BaseAddress;
        var createDto = new ArticleCreateDto
        {
            VendorId = element.VendorId,
            Number = element.Number,
            Title = element.Title,
            ArticleCategory = element.ArticleCategory,
            Manufacturer = element.Manufacturer,
            ApprovedForSale = element.ApprovedForSale,
            Price01 = element.Price01,
            Price02 = element.Price02,
            Description = element.Description
        };

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
        var updateDto = new ArticleUpdateDto
        {
            VendorId = element.VendorId,
            Number = element.Number,
            Title = element.Title,
            ArticleCategory = element.ArticleCategory,
            Manufacturer = element.Manufacturer,
            Created = element.Created,
            ApprovedForSale = element.ApprovedForSale,
            Sold = element.Sold,
            SoldAt = element.SoldAt,
            Settled = element.Settled,
            Price01 = element.Price01,
            Price02 = element.Price02,
            Description = element.Description
        };

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

    public bool FilterFunc(Manufacturer dto, string filterString)
    {
        if (string.IsNullOrWhiteSpace(filterString))
            return true;

        if (dto.Name.Contains(filterString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}

public class ApiQueryBuilder
{
    private readonly Dictionary<string, string?> _innerDict = new();

    public string? this[string key]
    {
        get => _innerDict.TryGetValue(key, out var value) ? value : null;
        set => _innerDict[key] = value;
    }

    public void Set(string key, string value)
    {
        _innerDict[key] = value;
    }

    public string Build()
    {
        var queryParts = GetQueryParts();
        var result = queryParts.Aggregate("", (current, next) => current + "&" + next);
        return result.TrimStart('&').Trim();
    }

    public string BuildFinal()
        => "?" + Build();

    private IEnumerable<string> GetQueryParts()
        => _innerDict.Where(p => p.Value != null).Select(item => $"{item.Key}={item.Value}");
}