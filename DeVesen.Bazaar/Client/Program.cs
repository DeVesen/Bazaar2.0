using DeVesen.Bazaar.Client;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Services;
using DeVesen.Bazaar.Client.State.Article;
using DeVesen.Bazaar.Client.State.ArticleCategory;
using DeVesen.Bazaar.Client.State.Import;
using DeVesen.Bazaar.Client.State.Manufacturer;
using DeVesen.Bazaar.Client.State.SalesCart;
using DeVesen.Bazaar.Client.State.Settlement;
using DeVesen.Bazaar.Client.State.Title;
using DeVesen.Bazaar.Client.State.VendorView;
using DeVesen.Bazaar.Client.Validator;
using DeVesen.Bazaar.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices()
                .ConfigureFluxor();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TokenAuthenticationStateProvider>());

builder.Services.AddTransient<ManufacturerFacade>();
builder.Services.AddTransient<ArticleCategoryFacade>();
builder.Services.AddTransient<VendorViewFacade>();
builder.Services.AddTransient<ArticleFacade>();
builder.Services.AddTransient<SettlementFacade>();

builder.Services.AddTransient<ImportFacade>();
builder.Services.AddTransient<SalesCartFacade>();
builder.Services.AddTransient<TitleFacade>();

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient(HubConnectionProviderExtensions.CreateVendorHubConnectionService)
                .AddTransient(HubConnectionProviderExtensions.CreateArticleHubConnectionService)
                .AddTransient(HubConnectionProviderExtensions.CreateManufacturerHubConnectionService)
                .AddTransient(HubConnectionProviderExtensions.CreateArticleCategoryHubConnectionService);

builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<NavigationService>();
builder.Services.AddTransient<DialogService>();
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
