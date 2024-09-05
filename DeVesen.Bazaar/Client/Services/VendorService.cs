﻿using System.Net.Http.Json;
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

    public async Task<Response<VendorView?>> GetByIdAsync(string id)
    {
        var requestUri = new UriBuilder(_httpClient.BaseAddress)
                                .AddUriPart(id)
                                .Build();

        if (string.IsNullOrWhiteSpace(id))
        {
            return Response<VendorView?>.Valid(null);
        }

        var dtoElement =
            await _httpClient.GetFromJsonAsync<VendorViewDto?>(requestUri);

        var domainElement = dtoElement != null
            ? MapToDomain(dtoElement)
            : null;

        return Response<VendorView?>.Valid(domainElement);
    }

    public async Task<Response<IEnumerable<VendorView>>> GetAllAsync(string? id = null, string? salutation = null, string? searchText = null)
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .SetQueryItem("Id", id)
                .SetQueryItem("Salutation", salutation)
                .SetQueryItem("SearchText", searchText)
                .Build();

            var dtoElements =
                await _httpClient.GetFromJsonAsync<IEnumerable<VendorViewDto>>(requestUri) ?? [];

            var domainElements = MapToDomain(dtoElements);

            return Response<IEnumerable<VendorView>>.Valid(domainElements.ToArray());
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<VendorView>>.Invalid(ex.Message);
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


    internal static IEnumerable<VendorView> MapToDomain(IEnumerable<VendorViewDto> elements)
        => elements.Select(MapToDomain);

    internal static VendorView MapToDomain(VendorViewDto dtoElement)
        => new()
        {
            Item = new Vendor
            {
                Id = dtoElement.Item.Id,
                Salutation = dtoElement.Item.Salutation,
                FirstName = dtoElement.Item.FirstName,
                LastName = dtoElement.Item.LastName,
                Address = dtoElement.Item.Address,
                EMail = dtoElement.Item.EMail,
                Note = dtoElement.Item.Note
            },
            Statistic = new VendorArticleStatistic
            {
                NotOpen = dtoElement.Statistic.NotOpen,
                Open = dtoElement.Statistic.Open,
                Sold = dtoElement.Statistic.Sold,
                Settled = dtoElement.Statistic.Settled,
                Turnover = dtoElement.Statistic.Turnover
            }
        };
}