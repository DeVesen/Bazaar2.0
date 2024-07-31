using System.Net.Http.Json;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Services;

public class StatisticsOverviewService
{
    private readonly HttpClient _httpClient;

    public StatisticsOverviewService(IWebAssemblyHostEnvironment hostEnvironment, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = hostEnvironment.GetApiEndpointUrl("api/v1/Vendor");
    }

    public async Task<IEnumerable<VendorOverviewItem>> GetVendorOverviewAsync()
    {
        var dtoList = await _httpClient.GetFromJsonAsync<IEnumerable<VendorDto>>("");

        return dtoList!.Select(element => new VendorOverviewItem
        {
            MasterData = new()
            {
                Id = element.Id,
                Salutation = element.Salutation,
                FirstName = element.FirstName,
                LastName = element.LastName,
                Address = element.Address,
                EMail = element.EMail,
                Phone = element.Phone,
                Note = element.Note
            },
            Stats = new()
            {
                Open = 20,
                Sold = 5,
                Settled = 0
            }
        });
    }
}