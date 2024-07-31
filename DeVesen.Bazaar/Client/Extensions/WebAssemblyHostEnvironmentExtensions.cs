using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace DeVesen.Bazaar.Client.Extensions;

public static class WebAssemblyHostEnvironmentExtensions
{
    public static Uri GetApiEndpointUrl(this IWebAssemblyHostEnvironment hostEnvironment, string path)
        => new(new Uri(hostEnvironment.BaseAddress), path);
}