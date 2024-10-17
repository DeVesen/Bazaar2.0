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

    public async Task<Response<long>> GetNextNumber()
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .AddUriPart("next-number")
                .Build();

            var dtoElement =
                await _httpClient.GetFromJsonAsync<NextArticleNumberDto>(requestUri);

            return dtoElement != null
                ? Response<long>.Valid(dtoElement.Number)
                : Response<long>.Invalid("Invalid response!");
        }
        catch (Exception ex)
        {
            return Response<long>.Invalid(ex.Message);
        }
    }

    public async Task<Response<Article?>> GetByNumber(long number)
    {
        var result = await GetAllAsync(number: number.ToString());

        return result.IsValid 
            ? Response<Article?>.Valid(result.Value.FirstOrDefault(p => p.Number == number))
            : Response<Article?>.Invalid(result.ErrorMessages);
    }

    public async Task<Response<IEnumerable<Article>>> GetAllAsync(string? vendorId = null, string? number = null, string? searchText = null)
    {
        try
        {
            var requestUri = new UriBuilder(_httpClient.BaseAddress)
                .SetQueryItem("VendorId", vendorId)
                .SetQueryItem("Number", number)
                .SetQueryItem("SearchText", searchText)
                .Build();

            var dtoElements =
                await _httpClient.GetFromJsonAsync<IEnumerable<ArticleDto>>(requestUri) ?? [];

            var domainElements = MapToDomain(dtoElements);

            return Response<IEnumerable<Article>>.Valid(domainElements.ToArray());
        }
        catch (Exception ex)
        {
            return Response<IEnumerable<Article>>.Invalid(ex.Message);
        }
    }

    public async Task<Response> CreateAsync(Article element, bool showResultSnackBar = true)
    {
        var requestUri = _httpClient.BaseAddress;
        var createDto = element.ToCreateDto();

        var response = await _httpClient.PostAsJsonAsync(requestUri, createDto);

        if (response.IsSuccessStatusCode)
        {
            if (showResultSnackBar)
            {
                _snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Description}' erfolgreich angelegt ...");
            }

            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        if (showResultSnackBar)
        {
            _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}{Environment.NewLine}{message!.Message}");
        }

        return Response.Invalid(message!.Message);
    }

    public async Task<Response> UpdateAsync(Article element)
    {
        var requestUri = _httpClient.BaseAddress + $"/{element.Id}";
        var updateDto = element.ToUpdateDto();

        var response = await _httpClient.PutAsJsonAsync(requestUri, updateDto);

        if (response.IsSuccessStatusCode)
        {
            _snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Description}' erfolgreich geändert ...");
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
            _snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Description}' erfolgreich gelöscht ...");
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        _snackBarService.AddError($"Ausnahmefehler {response.StatusCode}{Environment.NewLine}{message!.Message}");
        return Response.Invalid();
    }


    public async Task<Response> BookOrderAsync(IEnumerable<SalesOrderDto.Position> soldItems)
    {
        var soldItemArray = soldItems.ToArray();
        var requestUri = _httpClient.BaseAddress + "/book-sale";
        var response = await _httpClient.PostAsJsonAsync(requestUri, new SalesOrderDto(soldItemArray));

        if (response.IsSuccessStatusCode)
        {
            var itemsCount = soldItemArray.Length;
            var itemsTotal = soldItemArray.Sum(p => p.Price);

            _snackBarService.AddInfo($"{itemsCount} Artikel für {itemsTotal} € erfolgreich verkauft.");
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        return Response.Invalid(message!.Message);
    }

    public async Task<Response> ApproveAsync(long articleNumber, string vendorId)
    {
        var requestUri = _httpClient.BaseAddress + $"/{articleNumber}/approve-for/{vendorId}";
        var response = await _httpClient.PostAsync(requestUri, null);

        if (response.IsSuccessStatusCode)
        {
            return Response.Valid();
        }

        var message = await response.Content.ReadFromJsonAsync<FailedRequestMessage>();

        return Response.Invalid(message!.Message);
    }


    internal static IEnumerable<Article> MapToDomain(IEnumerable<ArticleDto> elements)
        => elements.Select(MapToDomain);

    internal static Article MapToDomain(ArticleDto dtoElement)
        => new()
        {
            Id = dtoElement.Id,
            VendorId = dtoElement.VendorId,
            Number = dtoElement.Number,

            ArticleCategory = dtoElement.ArticleCategory,
            Manufacturer = dtoElement.Manufacturer,
            Description = dtoElement.Description,

            Price01 = dtoElement.Price01,
            Price02 = dtoElement.Price02,
            Created = dtoElement.Created,

            ApprovedForSale = dtoElement.ApprovedForSale,
            Sold = dtoElement.Sold,
            SoldAt = dtoElement.SoldAt,
            Returned = dtoElement.Returned,
            Settled = dtoElement.Settled
        };
}