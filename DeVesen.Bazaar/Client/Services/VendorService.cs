using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Statistics;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class VendorService(IWebAssemblyHostEnvironment hostEnvironment,
                           HttpClient httpClient,
                           SnackBarService snackBarService) : BaseService(hostEnvironment, httpClient, "api/v1/Vendor")
{
    public async Task<(bool Found, Vendor Vendor)> GetByIdAsync(string id)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("by-id")
            .AddUriPart(id)
            .Build();

        var response = await GetFromJsonAsync<Vendor>(requestUri);

        return response.IsSuccess
            ? (true, response.Response)
            : (false, null!);
    }


    public async Task<(bool IsSuccess, Vendor Vendor, string ErrorMessage)> CreateAsync(Vendor element)
    {
        var requestUri = new UriBuilder(BaseAddress)
                                .Build();
        var createDto = element.ToCreateDto();

        var response = await PostAsJsonAsync<VendorCreateDto, Vendor>(requestUri, createDto);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo($"Verkäufer '{element.FirstName} {element.LastName}' erfolgreich angelegt ...");

            return (true, response.Response, string.Empty);
        }

        snackBarService.AddError($"Verkäufer '{element.FirstName} {element.LastName}' konnte nicht erstellt werden!");
        Console.WriteLine($"Create {element.FirstName} {element.LastName} Error Code: {response.StatusCode}");
        Console.WriteLine($"Create {element.FirstName} {element.LastName} Error Msg: {response.ResponseData}");

        return (false, null!, response.ResponseData);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> UpdateAsync(Vendor element)
    {
        var requestUri = new UriBuilder(BaseAddress)
                                .AddUriPart(element.Id)
                                .Build();
        var updateDto = element.ToUpdateDto();

        var response = await PutAsJsonAsync(requestUri, updateDto);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo("Erfolgreich aktualisiert ...");
            return (true, string.Empty);
        }

        snackBarService.AddError($"Verkäufer '{element.FirstName} {element.LastName}' konnte nicht geändert werden!");
        Console.WriteLine($"Update {element.FirstName} {element.LastName} Error Code: {response.StatusCode}");
        Console.WriteLine($"Update {element.FirstName} {element.LastName} Error Msg: {response.ResponseData}");

        return (false, response.ResponseData);
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAsync(Vendor element)
    {
        var requestUri = new UriBuilder(BaseAddress)
                                .AddUriPart(element.Id)
                                .Build();

        var response = await DeleteAsync(requestUri);

        if (response.IsSuccess)
        {
            snackBarService.AddInfo("Erfolgreich gelöscht ...");
            return (true, string.Empty);
        }

        snackBarService.AddError($"Verkäufer '{element.FirstName} {element.LastName}' konnte nicht gelöscht werden!");
        Console.WriteLine($"Delete {element.FirstName} {element.LastName} Error Code: {response.StatusCode}");
        Console.WriteLine($"Delete {element.FirstName} {element.LastName} Error Msg: {response.ResponseData}");

        return (false, response.ResponseData);
    }



    public async Task<(bool IsSuccess, IEnumerable<VendorOverviewItem> Vendors, string ErrorMessage)> GetOverviewAsync(string? id = null, string? searchText = null)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart("overview")
            .SetQueryItem("Id", id)
            .SetQueryItem("SearchText", searchText)
            .Build();

        var response =
            await GetFromJsonAsync<IEnumerable<VendorOverviewItem>>(requestUri);

        return response.IsSuccess
            ? (true, response.Response, string.Empty)
            : (false, [], response.ResponseData);
    }

    public async Task<(bool IsSuccess, CountsStatistics Counts, ValuesStatistics Values, string ErrorMessage)> GetStatisticsByVendor(string vendorId)
    {
        var requestUri = new UriBuilder(BaseAddress)
            .AddUriPart(vendorId)
            .AddUriPart("statistics")
            .Build();

        var response = await GetFromJsonAsync<VendorStatisticsDto>(requestUri);

        return response.IsSuccess
            ? (true, response.Response.Counts, response.Response.Values, string.Empty)
            : (false, null!, null!, response.ResponseData);
    }
}