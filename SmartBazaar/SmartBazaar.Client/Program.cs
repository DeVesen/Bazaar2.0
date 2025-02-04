using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SmartBazaar.Client.Services;
using SmartBazaar.Client.States.ArticleBrandState;
using SmartBazaar.Client.States.ArticleCategoryState;
using SmartBazaar.Client.States.ArticleState;
using SmartBazaar.Client.States.DealerInfoState;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Validator;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(_ => builder.Services);

builder.Services.AddMudServices()
                .AddFluxor(config => config
                    .ScanAssemblies(typeof(Program).Assembly)
                    .UseReduxDevTools());

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddScoped<IArticleService, ArticleService>()
                .AddScoped<IArticleBrandService, ArticleBrandService>()
                .AddScoped<IArticleCategoryService, ArticleCategoryService>()
                .AddScoped<IDealerService, DealerService>()
                .AddScoped<IArticleService, ArticleService>();

builder.Services.AddTransient<SnackBarService>()
                .AddTransient<DlgService>();

builder.Services.AddTransient<ArticleCategoryValidator>()
                .AddTransient<ArticleBrandValidator>()
                .AddTransient<ArticleCreateValidator>()
                .AddTransient<ArticleModifyValidator>()
                .AddTransient<ArticlePriceValidator>()
                .AddTransient<DealerValidator>();

builder.Services.AddTransient<ArticleBrandFacade>()
                .AddTransient<ArticleCategoryFacade>()
                .AddTransient<DealerInfoFacade>()
                .AddTransient<ArticleFacade>();

await builder.Build().RunAsync();
