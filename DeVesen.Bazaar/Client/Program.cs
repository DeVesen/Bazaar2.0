using DeVesen.Bazaar.Client;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Services;
using DeVesen.Bazaar.Client.State.ArticleOverview;
using DeVesen.Bazaar.Client.Validator;
using DeVesen.Bazaar.Shared.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices()
                .ConfigureFluxor();

builder.Services.AddTransient<ArticleOverviewFacade>();

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<NavigationService>();
builder.Services.AddTransient<SystemClock>();
builder.Services.AddTransient<SnackBarService>();

builder.Services.AddHttpClient<ArticleCategoryService>();
builder.Services.AddHttpClient<ManufacturerService>();
builder.Services.AddHttpClient<VendorService>();
builder.Services.AddHttpClient<ArticleService>();

builder.Services.AddTransient<ArticleCategoryValidator>();
builder.Services.AddTransient<ManufacturerValidator>();
builder.Services.AddTransient<VendorValidator>();
builder.Services.AddTransient<ArticleValidator>();

await builder.Build().RunAsync();
