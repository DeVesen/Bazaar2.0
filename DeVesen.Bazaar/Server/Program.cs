using DeVesen.Bazaar.Server;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Hubs;
using DeVesen.Bazaar.Server.Repository.LiteDb;
using DeVesen.Bazaar.Server.Services;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;
using DeVesen.Bazaar.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<ILiteDbEngine, LiteDbEngine>(_ => new LiteDbEngine(AppEnvironment.GetDataFilePath("bazaar.data.db")));

builder.Services.AddTransient<SystemClock>();

builder.Services.AddTransient<VendorHubContext>();
builder.Services.AddTransient<ArticleHubContext>();
builder.Services.AddTransient<ArticleCategoryHubContext>();
builder.Services.AddTransient<ManufacturerHubContext>();

builder.Services.AddTransient<IArticleCategoryRepository, ArticleCategoryLiteDbRepository>()
                .AddTransient<IManufacturerRepository, ManufacturerLiteDbRepository>()
                .AddTransient<IVendorRepository, VendorLiteDbRepository>()
                .AddTransient<IArticleRepository, ArticleLiteDbRepository>();

builder.Services.AddTransient<ArticleCategoryStorage>()
                .AddTransient<ManufacturerStorage>()
                .AddTransient<VendorStorage>()
                .AddTransient<ArticleStorage>();

builder.Services.AddTransient<ArticleCategoryValidator>()
                .AddTransient<ManufacturerValidator>()
                .AddTransient<VendorValidator>()
                .AddTransient<ArticleValidator>();

builder.Services.AddSingleton<ArticleNumberService>();

builder.Services.AddSignalR();

var app = builder.Build();

await app.Services.SetupAppEnvironment();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.MapHub<VendorHub>("/DataSync/Vendor");
app.MapHub<ArticleHub>("/DataSync/Article");
app.MapHub<ManufacturerHub>("/DataSync/Manufacturer");
app.MapHub<ArticleCategoryHub>("/DataSync/ArticleCategory");

app.MapFallbackToFile("index.html");

app.Run();
