using System.Net;
using DeVesen.Bazaar.Shared;
using System.Text;
using System.Text.Json;

namespace DeVesen.Bazaar.ClientSimulator;

internal class Program
{
    private static async Task Main()
    {
        Console.WriteLine("Client-Simulator started ...");
        Console.WriteLine("");

        await Task.Delay(2500);

        var vendorCreateResponse = await CreateVendor();

        for (var counter = 1; counter < 1000; counter++)
        {
            var element = new ArticleCreateDto
            {
                VendorId = vendorCreateResponse.Vendor.id,
                Number = 90000 + counter,
                ArticleCategory = "Test-Category",
                Manufacturer = "Test-Manufacturer",
                Description = "Test-Description",
                Price01 = 100 + counter
            };

            var result = await SendPostRequestAsync("Article", element);

            Console.WriteLine($"Send: {element.Number} ... {result.StatusCode}");

            await Task.Delay(200);
        }

        Console.WriteLine("Hello, World!");
    }


    public static async Task<(HttpStatusCode StatusCode, Vendor Vendor)> CreateVendor()
    {
        var vendorCreate = new VendorCreateDto
        {
            FirstName = "ClientSimulator",
            LastName = Guid.NewGuid().ToString(),
            Note = "Client - Simulator"
        };

        var result = await SendPostRequestAsync<Vendor>("Vendor", vendorCreate);

        return (result.StatusCode, result.Data);
    }


    public static async Task<(HttpStatusCode StatusCode, T Data)> SendPostRequestAsync<T>(string url, object data)
    {
        var result = await SendPostRequestAsync(url, data);

        var dataObj = JsonSerializer.Deserialize<T>(result.Data);

        return (result.StatusCode, dataObj!);
    }

    public static async Task<(HttpStatusCode StatusCode, string Data)> SendPostRequestAsync(string url, object data)
    {
        var baseUrl = $"https://localhost:7199/api/v1/{url}";

        using var client = new HttpClient();

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(baseUrl, content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        return (response.StatusCode, responseContent);
    }


    public class Vendor
    {
        public string id { get; set; }
    }
}