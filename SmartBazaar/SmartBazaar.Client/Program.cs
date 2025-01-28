using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SmartBazaar.Client.Services;
using SmartBazaar.Client.States.ArticleBrandState;
using SmartBazaar.Client.States.ArticleCategoryState;
using SmartBazaar.Client.Validator;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(_ => builder.Services);

builder.Services.AddMudServices()
                .AddFluxor(config => config
                    .ScanAssemblies(typeof(Program).Assembly)
                    .UseReduxDevTools());

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddScoped<IArticleService, ArticleHttpService>()
                .AddScoped<IArticleBrandService, ArticleBrandHttpService>()
                .AddScoped<IArticleCategoryService, ArticleCategoryHttpService>();

builder.Services.AddTransient<ISnackBarService, SnackBarService>()
                .AddTransient<IDlgService, DlgService>();

builder.Services.AddTransient<ArticleCategoryValidator>()
                .AddTransient<ArticleBrandValidator>()
                .AddTransient<ArticleDescriptionValidator>()
                .AddTransient<ArticlePriceValidator>();

builder.Services.AddTransient<ArticleBrandFacade>()
                .AddTransient<ArticleCategoryFacade>();

await builder.Build().RunAsync();
