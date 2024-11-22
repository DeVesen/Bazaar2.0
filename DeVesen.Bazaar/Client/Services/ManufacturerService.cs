using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class ManufacturerService(IWebAssemblyHostEnvironment hostEnvironment,
                                 HttpClient httpClient,
                                 SnackBarService snackBarService) : BaseService(hostEnvironment, httpClient, "api/v1/Manufacturer")
{
    public async Task<bool> ExistsAsync(string value)
    {
        var elementResult = await GetAllAsync(value);

        return elementResult.IsSuccess && elementResult.Elements.Any();
    }

    public async Task<(bool IsSuccess, IEnumerable<Manufacturer> Elements)> GetAllAsync(string? name = null, string? searchText = null)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .SetQueryItem("Name", name)
            .SetQueryItem("SearchText", searchText)
            .Build();

        var response = await GetFromJsonAsync<IEnumerable<Manufacturer>>(requestUri);

        return response.IsSuccess
            ? (true, response.Response)
            : (false, []);
    }


    public async Task<bool> CreateAsync(Manufacturer element)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .Build();

        var response = await PostAsJsonAsync(requestUri, element);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo($"Hersteller '{element.Name}' erfolgreich angelegt ...");
            return true;
        }

        snackBarService.AddError($"Hersteller '{element.Name}' konnte nicht erstellt werden!");

        Console.WriteLine($"Create {element.Name} Error Code: {response.StatusCode}");
        Console.WriteLine($"Create {element.Name} Error Msg: {response.ResponseData}");

        return false;
    }

    public async Task<bool> UpdateAsync(Manufacturer element)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(element.Id)
            .Build();

        var updateDto = element.ToUpdateDto();
        var response = await PutAsJsonAsync(requestUri, updateDto);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo($"Hersteller '{element.Name}' erfolgreich geändert ...");
            return true;
        }

        snackBarService.AddError($"Hersteller '{element.Name}' konnte nicht geändert werden!");
        Console.WriteLine($"Update {element.Name} Error Code: {response.StatusCode}");
        Console.WriteLine($"Update {element.Name} Error Msg: {response.ResponseData}");

        return false;
    }

    public async Task<bool> DeleteAsync(Manufacturer element)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(element.Id)
            .Build();

        var response = await DeleteAsync(requestUri);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo($"Hersteller '{element.Name}' erfolgreich gelöscht ...");
            return true;
        }

        snackBarService.AddError($"Hersteller '{element.Name}' konnte nicht gelöscht werden!");
        Console.WriteLine($"Delete {element.Name} Error Code: {response.StatusCode}");
        Console.WriteLine($"Delete {element.Name} Error Msg: {response.ResponseData}");

        return false;
    }
}