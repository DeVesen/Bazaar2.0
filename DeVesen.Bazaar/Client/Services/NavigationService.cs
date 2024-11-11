using DeVesen.Bazaar.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeVesen.Bazaar.Client.Services;

public class NavigationService(NavigationManager navigationManager, IJSRuntime jsRuntime)
{
    public string Uri => navigationManager.Uri;
    public string BaseUri => navigationManager.BaseUri;

    public void ToArticleOverview(string vendorId)
    {
        navigationManager.NavigateTo($"vendors/{vendorId}/articles");
    }

    public async Task ToVendorPrint(string vendorId)
    {
        navigationManager.NavigateTo($"{BaseUri}vendors/{vendorId}/print");
        //try
        //{
        //    await jsRuntime.InvokeAsync<object>("open", $"{BaseUri}vendors/{vendorId}/print");
        //}
        //catch
        //{
        //    // ignored
        //}
    }

    public async Task ToVendorPrintSettlement(string vendorId)
    {
        navigationManager.NavigateTo($"{BaseUri}vendors/{vendorId}/print/settlement");
        //try
        //{
        //    await jsRuntime.InvokeAsync<object>("open", $"{BaseUri}vendors/{vendorId}/print/settlement");
        //}
        //catch
        //{
        //    // ignored
        //}
    }

    public async Task ToArticleLabelPrint(string data)
    {
        navigationManager.NavigateTo($"{BaseUri}article-lables/{data}");
        //try
        //{
        //    var targetUrl = $"{BaseUri}article-lables/{data}";

        //    await jsRuntime.InvokeAsync<object>("open", targetUrl, "_blank");
        //}
        //catch
        //{
        //    // ignored
        //}
    }

    public void ToModifyOverview(string vendorId)
    {
        navigationManager.NavigateTo($"vendors/{vendorId}/modify");
    }
}