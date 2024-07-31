using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Repository;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Server.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IArticleCategoryRepository, ArticleCategoryRepository>();
builder.Services.AddSingleton<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddSingleton<IVendorRepository, VendorRepository>();
builder.Services.AddSingleton<IArticleRepository, ArticleRepository>();

builder.Services.AddTransient<ArticleCategoryStorage>();
builder.Services.AddTransient<ManufacturerStorage>();
builder.Services.AddTransient<VendorStorage>();
builder.Services.AddTransient<ArticleStorage>();

builder.Services.AddTransient<ArticleCategoryValidator>();
builder.Services.AddTransient<ManufacturerValidator>();
builder.Services.AddTransient<VendorValidator>();
builder.Services.AddTransient<ArticleValidator>();

var app = builder.Build();

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
app.MapFallbackToFile("index.html");

app.Run();
