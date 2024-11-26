using Microsoft.AspNetCore.Components;

namespace DeVesen.Bazaar.Client.Services;

public class NavigationService(NavigationManager navigationManager)
{
    public string BaseUri => navigationManager.BaseUri;

    public void ToArticleOverview(string vendorId)
    {
        navigationManager.NavigateTo($"vendors/{vendorId}/articles");
    }

    public void ToVendorPrint(string vendorId)
    {
        navigationManager.NavigateTo($"{BaseUri}vendors/{vendorId}/print");
    }

    public void ToVendorPrintSettlement(string vendorId)
    {
        navigationManager.NavigateTo($"{BaseUri}vendors/{vendorId}/print/settlement");
    }

    public void ToArticleLabelPrint(string data)
    {
        navigationManager.NavigateTo($"{BaseUri}article-lables/{data}");
    }

    public void ToModifyOverview(string vendorId)
    {
        navigationManager.NavigateTo($"vendors/{vendorId}/modify");
    }
}