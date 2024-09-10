using DeVesen.Bazaar.Client.Components.Dialogs;
using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Models;
using MudBlazor;
using static DeVesen.Bazaar.Client.Services.DialogService;

namespace DeVesen.Bazaar.Client.Services;

public class DialogService(IDialogService dialogService)
{
    public enum MessageBoxResultType
    {
        Yes,
        No,
        Cancel
    }


    public async Task CreateVendorAsync()
    {
        var options = GetDefaultOptions();
        var forceNext = false;

        do
        {
            var result = await dialogService.ShowAsync<VendorCreateDialog>("Verkäufer anlegen", options)
                                            .WaitForResult<VendorCreateDialog.OkResult>();

            if (result.Canceled is false)
            {
                forceNext = result.Data.ForceNext;
            }

        } while (forceNext);
    }

    public async Task ModifyVendorAsync(Vendor vendor)
    {
        var options = GetDefaultOptions();
        var parameters = new DialogParameters<VendorEditDialog> { { x => x.Item, vendor } };

        await dialogService.ShowAsync<VendorEditDialog>("Verkäufer ändern", parameters, options)
                           .WaitForResult();
    }

    public async Task<bool> PayOutVendorAsync(double openSales, double shareOfSales, double articleCommission)
    {
        var options = GetDefaultOptions(true);
        var parameters = new DialogParameters<ArticlePayOutDialog>
        {
            { x => x.OpenSales, openSales },
            { x => x.ShareOfSales, shareOfSales },
            { x => x.ArticleCommission, articleCommission }
        };

        return await dialogService.ShowAsync<ArticlePayOutDialog>("Artikel zurückgeben", parameters, options)
                                  .WaitForResult();
    }


    public async Task CreateArticleAsync(string vendorId)
    {
        var options = GetDefaultOptions();
        var forceNext = false;

        do
        {
            var parameters = new DialogParameters<ArticleCreateDialog>
            {
                { x => x.ForceNext, forceNext },
                { x => x.VendorId, vendorId }
            };

            var dlg = await dialogService.ShowAsync<ArticleCreateDialog>("Artikel anlegen", parameters, options);
            var result = await dlg.Result;

            forceNext = result!.Canceled is false && (bool)result.Data!;

        } while (forceNext);
    }

    public async Task ModifyArticleAsync(Article article)
    {
        var options = GetDefaultOptions();
        var parameters = new DialogParameters<ArticleEditDialog>
        {
            { x => x.Item, article }
        };

        await dialogService.ShowAsync<ArticleEditDialog>("Artikel ändern", parameters, options)
                           .WaitForResult();
    }

    public async Task ImportArticleAsync(string vendorId)
    {
        var options = GetDefaultOptions();
        var parameters = new DialogParameters<ArticleImportDialog> { { x => x.VendorId, vendorId } };

        await dialogService.ShowAsync<ArticleImportDialog>("Artikel importieren", parameters, options)
            .WaitForResult();
    }

    public async Task ApproveArticleAsync(string vendorId)
    {
        var options = GetDefaultOptions();
        var parameters = new DialogParameters<ArticleApprovalDialog> { { x => x.VendorId, vendorId } };

        await dialogService.ShowAsync<ArticleApprovalDialog>("Artikel Freigeben", parameters, options)
            .WaitForResult();
    }

    public async Task GiveBackArticleAsync(string vendorId)
    {
        var options = GetDefaultOptions();
        var parameters = new DialogParameters<ArticleGiveBackDialog> { { x => x.VendorId, vendorId } };

        await dialogService.ShowAsync<ArticleGiveBackDialog>("Artikel zurückgeben", parameters, options)
            .WaitForResult();
    }


    public async Task CreateArticleCategoryAsync()
    {
        var options = GetDefaultOptions();
        var forceNext = false;

        do
        {
            var parameters = new DialogParameters<ArticleCategoryCreateDialog> { { x => x.ForceNext, forceNext } };

            var dlg = await dialogService.ShowAsync<ArticleCategoryCreateDialog>("Artikel-Kategorie anlegen", parameters, options);
            var result = await dlg.Result;

            forceNext = result!.Canceled is false && (bool)result.Data!;
        } while (forceNext);
    }

    public async Task ModifyArticleCategoryAsync(ArticleCategory item)
    {
        var options = GetDefaultOptions();
        var parameters = new DialogParameters<ArticleCategoryEditDialog> { { x => x.Item, item } };

        await dialogService.ShowAsync<ArticleCategoryEditDialog>("Artikel-Kategorie ändern", parameters, options)
                           .WaitForResult();
    }


    public async Task CreateManufacturerAsync()
    {
        var options = GetDefaultOptions();
        var forceNext = false;

        do
        {
            var parameters = new DialogParameters<ManufacturerCreateDialog> { { x => x.ForceNext, forceNext } };

            var result = await dialogService.ShowAsync<ManufacturerCreateDialog>("Artikel-Kategorie anlegen", parameters, options)
                .WaitForResult<ManufacturerCreateDialog.OkResult>();

            if (result.Canceled is false)
            {
                forceNext = result.Data.ForceNext;
            }

        } while (forceNext);
    }

    public async Task ModifyManufacturerAsync(Manufacturer item)
    {
        var options = GetDefaultOptions();
        var parameters = new DialogParameters<ManufacturerEditDialog>
        {
            { x => x.Item, item }
        };

        await dialogService.ShowAsync<ManufacturerEditDialog>("Artikel-Kategorie ändern", parameters, options)
                           .WaitForResult();
    }


    public async Task<MessageBoxResultType> ShowMessageBox(string message, string? title = null, string yesText = "OK", string? noText = null, string? cancelText = null, bool? fullWidth = null)
    {
        var options = GetDefaultOptions(fullWidth);
        var result = await dialogService.ShowMessageBox(title, message, yesText, noText, cancelText, options);

        if (result.HasValue is false)
        {
            return MessageBoxResultType.Cancel;
        }

        return result.Value ? MessageBoxResultType.Yes : MessageBoxResultType.No;
    }


    private static DialogOptions GetDefaultOptions(bool? fullWidth = null)
        => new()
        {
            CloseOnEscapeKey = true,
            BackdropClick = false,
            FullWidth = fullWidth
        };
}

public static class DialogServiceExtensions
{
    public static async Task<bool> IsYes(this Task<MessageBoxResultType> msgResult)
    {
        return (await msgResult) == MessageBoxResultType.Yes;
    }
}