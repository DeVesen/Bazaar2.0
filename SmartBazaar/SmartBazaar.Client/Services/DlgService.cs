using Microsoft.AspNetCore.Components;
using MudBlazor;
using SmartBazaar.Client.Components.ArticleBrand;
using SmartBazaar.Client.Components.ArticleCategory;

namespace SmartBazaar.Client.Services;

public class DlgService(IDialogService dialogService) : IDlgService
{
    public async Task ArticleBrandCreate()
    {
        await ShowAsync<ArticleBrandCreateDialog>("Hersteller anlegen", MaxWidth.ExtraSmall, true);
    }

    public async Task ArticleBrandModify(Models.ArticleBrand element)
    {
        var parameters = new DialogParameters<ArticleBrandModifyDialog> { { x => x.ArticleBrand, element } };

        await ShowAsync("Hersteller ändern", parameters, MaxWidth.ExtraSmall, true);
    }


    public async Task ArticleCategoryCreate()
    {
        await ShowAsync<ArticleCategoryCreateDialog>("Kategorie anlegen", MaxWidth.ExtraSmall, true);
    }

    public async Task ArticleCategoryModify(Models.ArticleCategory element)
    {
        var parameters = new DialogParameters<ArticleCategoryModifyDialog> { { x => x.ArticleCategory, element } };

        await ShowAsync("Kategorie ändern", parameters, MaxWidth.ExtraSmall, true);
    }


    private async Task<bool> ShowAsync<TDialog>(string title, MaxWidth? maxWidth = null, bool? fullWidth = null, bool? fullScreen = null, bool? noHeader = null)
        where TDialog : IComponent
        => await ShowAsync(title, new DialogParameters<TDialog>(), maxWidth, fullWidth, fullScreen, noHeader);

    private async Task<bool> ShowAsync<TDialog>(string title,
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
        var result = await dlg.Result;

        return result!.Canceled is false;
    }
}

public interface IDlgService
{
    public Task ArticleBrandCreate();
    public Task ArticleBrandModify(Models.ArticleBrand element);

    public Task ArticleCategoryCreate();
    public Task ArticleCategoryModify(Models.ArticleCategory element);
}


public class SnackBarService(ISnackbar mudSnackBar) : ISnackBarService
{
    private const int DefaultVisibleStateDuration = 2500;


    public void AddInfo(string message)
    {
        AddInfo(message, Guid.NewGuid().ToString());
    }

    public void AddInfo(string message, string key)
    {
        AddInfo(message, key, ElementViewPositions.BottomRight);
    }

    public void AddInfo(string message, string key, ElementViewPositions position)
    {
        AddSnackBar(Severity.Info, message, key, position);
    }


    private void AddSnackBar(Severity severity, string message, string key, ElementViewPositions position)
    {
        mudSnackBar.Configuration.PositionClass = GetPositionDetails(position);
        mudSnackBar.Add(
            message,
            severity,
            config =>
            {
                config.VisibleStateDuration = DefaultVisibleStateDuration;
            },
            key);
    }


    private static string GetPositionDetails(ElementViewPositions position)
        => position switch
        {
            ElementViewPositions.TopStart => Defaults.Classes.Position.TopStart,
            ElementViewPositions.TopLeft => Defaults.Classes.Position.TopLeft,
            ElementViewPositions.TopCenter => Defaults.Classes.Position.TopCenter,
            ElementViewPositions.TopRight => Defaults.Classes.Position.TopRight,
            ElementViewPositions.TopEnd => Defaults.Classes.Position.TopEnd,
            ElementViewPositions.BottomStart => Defaults.Classes.Position.BottomStart,
            ElementViewPositions.BottomLeft => Defaults.Classes.Position.BottomLeft,
            ElementViewPositions.BottomCenter => Defaults.Classes.Position.BottomCenter,
            ElementViewPositions.BottomRight => Defaults.Classes.Position.BottomRight,
            ElementViewPositions.BottomEnd => Defaults.Classes.Position.BottomEnd,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
}

public interface ISnackBarService
{
    public void AddInfo(string message);
}

public enum ElementViewPositions
{
    TopStart,
    TopLeft,
    TopCenter,
    TopRight,
    TopEnd,

    BottomStart,
    BottomLeft,
    BottomCenter,
    BottomRight,
    BottomEnd
}