using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;

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

    public async Task<Response<VendorView>> GetByIdAsync(string id)
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .AddUriPart(id)
                .Build();

            if (string.IsNullOrWhiteSpace(id))
            {
                return Response<VendorView>.Invalid("Verkäufer nicht gefunden!");
            }

            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode is false)
            {
                return Response<VendorView>.Invalid("Verkäufer nicht gefunden!");
            }

            var dtoElement = await response.Content.ReadFromJsonAsync<VendorViewDto>();
            var domainElement = MapToDomain(dtoElement!);

            return Response<VendorView>.Valid(domainElement);
        }
        catch (Exception ex)
        {
            return Response<VendorView>.Invalid(ex.Message);
        }
    }

    public async Task<Response<IEnumerable<VendorView>>> GetAllAsync(string? id = null, string? searchText = null)
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .SetQueryItem("Id", id)
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


    public async Task<Response> ApproveAsync(string vendorId, long articleNumber)
    {
        var requestUri = _httpClient.BaseAddress + $"/{vendorId}/approve/{articleNumber}";
        var response = await _httpClient.PostAsync(requestUri, null);

        if (response.IsSuccessStatusCode)
        {
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        return Response.Invalid(message!.Message);
    }

    public async Task<Response> GiveBackArticleAsync(string vendorId, long articleNumber)
    {
        var requestUri = _httpClient.BaseAddress + $"/{vendorId}/giveback/{articleNumber}";
        var response = await _httpClient.PostAsync(requestUri, null);

        if (response.IsSuccessStatusCode)
        {
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        return Response.Invalid(message!.Message);
    }

    public async Task<Response> SettleAsync(string vendorId, IEnumerable<string> actionArticleIds)
    {
        var requestUri = _httpClient.BaseAddress + $"/{vendorId}/settle";
        var response = await _httpClient.PostAsJsonAsync(requestUri, actionArticleIds);

        if (response.IsSuccessStatusCode)
        {
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        return Response.Invalid(message!.Message);
    }


    internal static IEnumerable<VendorView> MapToDomain(IEnumerable<VendorViewDto> elements)
        => elements.Select(MapToDomain);

    internal static VendorView MapToDomain(VendorViewDto dtoElement)
        => new()
        {
            Item = new Vendor
            {
                Id = dtoElement.Item.Id,
                FirstName = dtoElement.Item.FirstName,
                LastName = dtoElement.Item.LastName,
                Address = dtoElement.Item.Address,
                EMail = dtoElement.Item.EMail,
                Note = dtoElement.Item.Note,
                OfferUnitPrice = dtoElement.Item.OfferUnitPrice,
                SalesShare = dtoElement.Item.SalesShare
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

public class StatisticService
{
    private readonly HttpClient _httpClient;

    public StatisticService(IWebAssemblyHostEnvironment hostEnvironment, HttpClient httpClient, SnackBarService snackBarService)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/Statistic");
    }

    public async Task<Response<StatisticSummeryView>> Get()
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .Build();

            var domainElement =
                await _httpClient.GetFromJsonAsync<StatisticSummeryView>(requestUri);

            if (domainElement == null)
            {
                return Response<StatisticSummeryView>.Invalid("No data reseived!");
            }

            return Response<StatisticSummeryView>.Valid(domainElement);
        }
        catch (Exception ex)
        {
            return Response<StatisticSummeryView>.Invalid(ex.Message);
        }
    }
}