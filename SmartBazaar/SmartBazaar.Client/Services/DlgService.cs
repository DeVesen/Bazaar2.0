using Microsoft.AspNetCore.Components;
using MudBlazor;
using SmartBazaar.Client.Components.Article;
using SmartBazaar.Client.Components.ArticleBrand;
using SmartBazaar.Client.Components.ArticleCategory;
using SmartBazaar.Client.Components.Dealer;
using SmartBazaar.Shared.Models;
using SmartBazaar.Shared.Types;

namespace SmartBazaar.Client.Services;

public class DlgService(IDialogService dialogService)
{
    public async Task ArticleBrandCreateAsync()
    {
        await ShowAsync<ArticleBrandCreateDialog>("Hersteller anlegen", MaxWidth.ExtraSmall, true);
    }

    public async Task ArticleBrandModifyAsync(string articleBrandId)
    {
        var parameters = new DialogParameters<ArticleBrandModifyDialog> { { x => x.ArticleBrandId, articleBrandId } };

        await ShowAsync("Hersteller ändern", parameters, MaxWidth.ExtraSmall, true);
    }


    public async Task ArticleCategoryCreateAsync()
    {
        await ShowAsync<ArticleCategoryCreateDialog>("Kategorie anlegen", MaxWidth.ExtraSmall, true);
    }

    public async Task ArticleCategoryModifyAsync(string articleCategoryId)
    {
        var parameters = new DialogParameters<ArticleCategoryModifyDialog> { { x => x.ArticleCategoryId, articleCategoryId } };

        await ShowAsync("Kategorie ändern", parameters, MaxWidth.ExtraSmall, true);
    }


    public async Task DealerCreateAsync()
    {
        await ShowAsync<DealerCreateDialog>("Verkäufer anlegen", MaxWidth.Small, true);
    }

    public async Task DealerModifyAsync(string dealerId)
    {
        var parameters = new DialogParameters<DealerModifyDialog> { { x => x.DealerId, dealerId } };

        await ShowAsync("Verkäufer ändern", parameters, MaxWidth.Small, true);
    }


    public async Task ArticleCreateAsync(string vendorId)
    {
        var parameters = new DialogParameters<ArticleCreateDialog> { { x => x.VendorId, vendorId } };

        await ShowAsync("Artikel anlegen", parameters, MaxWidth.Small, true);
    }

    public async Task ArticleModifyAsync(string articleId)
    {
        var parameters = new DialogParameters<ArticleModifyDialog> { { x => x.ArticleId, articleId } };

        await ShowAsync("Artikel ändern", parameters, MaxWidth.Small, true);
    }


    public async Task<MessageBoxResultType> ShowMessageBoxAsync(string message, string? title = null, string yesText = "OK", string? noText = null, string? cancelText = null, bool? fullWidth = null)
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = false,
            BackdropClick = false,
            FullWidth = fullWidth,
            FullScreen = fullWidth
        };
        var result = await dialogService.ShowMessageBox(title, message, yesText, noText, cancelText, options);

        if (result.HasValue is false)
        {
            return MessageBoxResultType.Cancel;
        }

        return result.Value ? MessageBoxResultType.Yes : MessageBoxResultType.No;
    }


    private async Task ShowAsync<TDialog>(string title, MaxWidth? maxWidth = null, bool? fullWidth = null,
        bool? fullScreen = null, bool? noHeader = null)
        where TDialog : IComponent
    {
        await ShowAsync(title, new DialogParameters<TDialog>(), maxWidth, fullWidth, fullScreen, noHeader);
    }

    private async Task ShowAsync<TDialog>(string title,
        DialogParameters<TDialog> parameter,
        MaxWidth? maxWidth = null, bool? fullWidth = null, bool? fullScreen = null, bool? noHeader = null)
        where TDialog : IComponent
    {
        var dlgOptions = new DialogOptions
        {
            CloseOnEscapeKey = false,
            BackdropClick = false,
            FullWidth = fullWidth,
            FullScreen = fullScreen,
            NoHeader = noHeader,
            MaxWidth = maxWidth
        };

        var dlg = await dialogService.ShowAsync<TDialog>(title, parameter, dlgOptions);
        _ = await dlg.Result;
    }
}