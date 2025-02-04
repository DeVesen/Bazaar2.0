using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class ArticleService(IWebAssemblyHostEnvironment hostEnvironment,
                            HttpClient httpClient,
                            SnackBarService snackBarService) : BaseService(hostEnvironment, httpClient, "api/v1/Article")
{
    public async Task<(bool IsSuccess, Article Article, string ErrorMessage)> GetByNumber(long number)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("by-number")
            .AddUriPart(number)
            .Build();

        var response = await GetFromJsonAsync<Article>(requestUri);

        return response.IsSuccess
            ? (true, response.Response, string.Empty)
            : (false, null!, response.ResponseData);
    }

    public async Task<(bool IsSuccess, IEnumerable<Article> Articles, string ErrorMessage)> GetAllAsync(string? vendorId = null, string? number = null, string? searchText = null)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("list")
            .SetQueryItem("DealerId", vendorId)
            .SetQueryItem("Number", number)
            .SetQueryItem("SearchText", searchText)
            .Build();

        var response = await GetFromJsonAsync<IEnumerable<Article>>(requestUri);

        return response.IsSuccess
            ? (true, response.Response, string.Empty)
            : (false, [], response.ResponseData);
    }


    public async Task<(bool IsSuccess, string ErrorMessage)> CreateAsync(Article element, bool showResultSnackBar = true)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .Build();

        var createDto = element.ToCreateDto();
        var response = await PostAsJsonAsync(requestUri, createDto);

        if (response.IsSuccess)
        {
            if (showResultSnackBar)
            {
                snackBarService.AddInfo($"Artikel '{element.Number}' erfolgreich angelegt ...");
            }

            return (true, string.Empty);
        }

        if (showResultSnackBar)
        {
            snackBarService.AddError($"Artikel '{element.Number}' konnte nicht erstellt werden!");
        }

        Console.WriteLine($"Create '{element.Number}' Error Code: {response.StatusCode}");
        Console.WriteLine($"Create '{element.Number}' Error Msg: {response.ResponseData}");

        return (false, response.ResponseData);
    }

    public async Task<bool> UpdateAsync(Article element)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(element.Id)
            .Build();

        var updateDto = element.ToUpdateDto();
        var response = await PutAsJsonAsync(requestUri, updateDto);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Description}' erfolgreich geändert ...");
            return true;
        }

        snackBarService.AddError($"Artikel '{element.Number}' konnte nicht geändert werden!");
        Console.WriteLine($"Update {element.Number} Error Code: {response.StatusCode}");
        Console.WriteLine($"Update {element.Number} Error Msg: {response.ResponseData}");

        return false;
    }

    public async Task<bool> DeleteAsync(Article element)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(element.Id)
            .Build();

        var response = await DeleteAsync(requestUri);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo($"Artikel '{element.Number}' - '{element.Description}' erfolgreich gelöscht ...");
            return true;
        }

        snackBarService.AddError($"Artikel '{element.Number}' konnte nicht gelöscht werden!");
        Console.WriteLine($"Delete {element.Number} Error Code: {response.StatusCode}");
        Console.WriteLine($"Delete {element.Number} Error Msg: {response.ResponseData}");

        return false;
    }


    public async Task<(bool IsSuccess, long NewNumber)> GetNextNumber()
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("next-number")
            .Build();

        var response = await GetFromJsonAsync<NextArticleNumberDto>(requestUri);

        if (response.IsSuccess is false)
        {
            return (response.IsSuccess, response.Response.Number);
        }

        snackBarService.AddError($"Es konnte keine neue Nummer ermittelt werden!");
        Console.WriteLine($"NextNumber Error Code: {response.StatusCode}");
        Console.WriteLine($"NextNumber Error Msg: {response.ResponseData}");

        return (response.IsSuccess, response.Response.Number);
    }

    public async Task<bool> BookOrderAsync(IEnumerable<SalesOrderDto.Position> soldItems)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("book-sale")
            .Build();

        var soldItemArray = soldItems.ToArray();
        var postData = new SalesOrderDto(soldItemArray);
        var response = await PostAsJsonAsync(requestUri, postData);

        if (response.IsSuccess is false)
        {
            snackBarService.AddInfo("Artikel konnte(n) nicht verkauft werden!");
            Console.WriteLine($"Sales Error Code: {response.StatusCode}");
            Console.WriteLine($"Sales Error Msg: {response.ResponseData}");

            return false;
        }

        var itemsCount = soldItemArray.Length;
        var itemsTotal = soldItemArray.Sum(p => p.Price);

        snackBarService.AddInfo($"{itemsCount} Artikel für {itemsTotal} € erfolgreich verkauft.");

        return true;

    }

    public async Task<(bool IsSuccess, string ErrorMessage)> ApproveAsync(long articleNumber, string vendorId)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(articleNumber)
            .AddUriPart("approve-for")
            .AddUriPart(vendorId)
            .Build();

        var response = await PostAsync(requestUri);

        if (response.IsSuccess)
        {
            return (true, string.Empty);
        }

        Console.WriteLine($"Approve {articleNumber} Error Code: {response.StatusCode}");
        Console.WriteLine($"Approve {articleNumber} Error Msg: {response.ResponseData}");

        return (false, response.ResponseData);

    }
}