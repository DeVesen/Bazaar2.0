using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Shared.Events;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public class ArticleViewFacade
{
    private readonly IDispatcher _dispatcher;
    private readonly VendorHubConnectionService _vendorHub;
    private readonly ArticleHubConnectionService _articleHub;

    public ArticleViewFacade(IDispatcher dispatcher,
                             VendorHubConnectionService vendorHub,
                             ArticleHubConnectionService articleHub)
    {
        _dispatcher = dispatcher;
        _vendorHub = vendorHub;
        _articleHub = articleHub;
    }

    public void Fetch(string? vendorId = null, string? number = null, string? searchText = null)
        => _dispatcher.Dispatch(new ArticleViewActions.Fetch(vendorId, number, searchText));

    public void Clear()
        => _dispatcher.Dispatch(new ArticleViewActions.Clear());


    public async Task StartCallbacks()
    {
        await _vendorHub.StartAsync();
        await _articleHub.StartAsync();

        _vendorHub.RegisterOnRemoved(OnVendorRemoved);

        _articleHub.RegisterOnAdded(OnArticleAdded);
        _articleHub.RegisterOnUpdated(OnArticleUpdated);
        _articleHub.RegisterOnStatusChanged(OnArticleStatusChanged);
        _articleHub.RegisterOnRemoved(OnArticleRemoved);
    }

    public async Task StopCallbacks()
    {
        await _vendorHub.StopAsync();
        await _articleHub.StopAsync();
    }


    private void OnVendorRemoved(VendorRemovedArgs args)
    {
        _dispatcher.Dispatch(new ArticleViewActions.SetBadVendor(args.Id));
    }


    private void OnArticleAdded(IEnumerable<ArticleAddedArgs> args)
    {
        foreach (var arg in args)
        {
            _dispatcher.Dispatch(new ArticleViewActions.SetItem(arg.Article.ToModel()));
        }
    }

    private void OnArticleUpdated(IEnumerable<ArticleUpdatedArgs> args)
    {
        foreach (var arg in args)
        {
            _dispatcher.Dispatch(new ArticleViewActions.SetItem(arg.Article.ToModel()));
        }
    }

    private void OnArticleStatusChanged(IEnumerable<ArticleStatusChangedArgs> args)
    {
        foreach (var arg in args)
        {
            _dispatcher.Dispatch(new ArticleViewActions.SetItem(arg.Article.ToModel()));
        }
    }

    private void OnArticleRemoved(IEnumerable<ArticleRemovedArgs> args)
    {
        foreach (var arg in args)
        {
            _dispatcher.Dispatch(new ArticleViewActions.SetItem(arg.Article.ToModel()));
        }
    }
}
