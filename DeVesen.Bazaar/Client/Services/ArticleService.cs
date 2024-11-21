using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class ArticleService(IWebAssemblyHostEnvironment hostEnvironment,
                            HttpClient httpClient,
                            SnackBarService snackBarService) : BaseService(hostEnvironment, httpClient, "api/v1/Article")
{
    public async Task<Response<Article>> GetByNumber(long number)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("by-number")
            .AddUriPart(number)
            .Build();

        var response = await GetFromJsonAsync<Article>(requestUri);

        return response.IsSuccess
            ? Response<Article>.Valid(response.ResponsedData)
            : Response<Article>.Invalid(response.ErrorMsg);
    }

    public async Task<Response<IEnumerable<Article>>> GetAllAsync(string? vendorId = null, string? number = null, string? searchText = null)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("list")
            .SetQueryItem("VendorId", vendorId)
            .SetQueryItem("Number", number)
            .SetQueryItem("SearchText", searchText)
            .Build();

        var response = await GetFromJsonAsync<IEnumerable<Article>>(requestUri);

        return response.IsSuccess
            ? Response<IEnumerable<Article>>.Valid(response.ResponsedData)
            : Response<IEnumerable<Article>>.Invalid(response.ErrorMsg);
    }


    public async Task<Response> CreateAsync(Article element, bool showResultSnackBar = true)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .Build();

        var createDto = element.ToCreateDto();
        var response = await PostAsJsonAsync(requestUri, createDto);

        if (response.IsSuccess)
        {
            if (showResultSnackBar)
            {
                snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Description}' erfolgreich angelegt ...");
            }

            return Response.Valid();
        }

        if (showResultSnackBar)
        {
            snackBarService.AddError($"Ausnahmefehler {response.StatusCode}{Environment.NewLine}{response.ErrorMsg}");
        }

        return Response.Invalid(response.ErrorMsg);
    }

    public async Task<Response> UpdateAsync(Article element)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(element.Id)
            .Build();

        var updateDto = element.ToUpdateDto();
        var response = await PutAsJsonAsync(requestUri, updateDto);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Description}' erfolgreich geändert ...");
            return Response.Valid();
        }

        snackBarService.AddError($"Ausnahmefehler {response.StatusCode}{Environment.NewLine}{response.ErrorMsg}");

        return Response.Invalid(response.ErrorMsg);
    }

    public async Task<Response> DeleteAsync(Article element)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(element.Id)
            .Build();

        var response = await base.DeleteAsync(requestUri);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Description}' erfolgreich gelöscht ...");
            return Response.Valid();
        }

        snackBarService.AddError($"Ausnahmefehler {response.StatusCode}{Environment.NewLine}{response.ErrorMsg}");

        return Response.Invalid(response.ErrorMsg);
    }


    public async Task<Response<long>> GetNextNumber()
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("next-number")
            .Build();

        var response = await GetFromJsonAsync<NextArticleNumberDto>(requestUri);

        return response.IsSuccess
            ? Response<long>.Valid(response.ResponsedData.Number)
            : Response<long>.Invalid(response.ErrorMsg);
    }

    public async Task<Response> BookOrderAsync(IEnumerable<SalesOrderDto.Position> soldItems)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("book-sale")
            .Build();

        var soldItemArray = soldItems.ToArray();
        var postData = new SalesOrderDto(soldItemArray);
        var response = await PostAsJsonAsync(requestUri, postData);

        if (response.IsSuccess is false)
        {
            return Response.Invalid(response.ErrorMsg);
        }

        var itemsCount = soldItemArray.Length;
        var itemsTotal = soldItemArray.Sum(p => p.Price);

        snackBarService.AddInfo($"{itemsCount} Artikel für {itemsTotal} € erfolgreich verkauft.");

        return Response.Valid();

    }

    public async Task<Response> ApproveAsync(long articleNumber, string vendorId)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(articleNumber)
            .AddUriPart("approve-for")
            .AddUriPart(vendorId)
            .Build();

        var response = await PostAsync(requestUri);

        return response.IsSuccess
            ? Response.Valid()
            : Response.Invalid(response.ErrorMsg);
    }
}