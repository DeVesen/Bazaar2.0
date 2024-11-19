using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Shared.Events;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public class ArticleViewFacade(IDispatcher dispatcher,
                               VendorHubConnectionService vendorHub,
                               ArticleHubConnectionService articleHub)
{
    public void Fetch(string? vendorId = null, string? number = null, string? searchText = null)
        => dispatcher.Dispatch(new ArticleViewActions.Fetch(vendorId, number, searchText));

    public void Clear()
        => dispatcher.Dispatch(new ArticleViewActions.Clear());


    public async Task StartCallbacks()
    {
        await vendorHub.StartAsync();
        await articleHub.StartAsync();

        vendorHub.RegisterOnRemoved(OnVendorRemoved);

        articleHub.RegisterOnAdded(OnArticleAdded);
        articleHub.RegisterOnUpdated(OnArticleUpdated);
        articleHub.RegisterOnStatusChanged(OnArticleStatusChanged);
        articleHub.RegisterOnRemoved(OnArticleRemoved);
    }

    public async Task StopCallbacks()
    {
        await vendorHub.StopAsync();
        await articleHub.StopAsync();
    }


    private void OnVendorRemoved(VendorRemovedArgs args)
    {
        dispatcher.Dispatch(new ArticleViewActions.SetBadVendor(args.Id));
    }


    private void OnArticleAdded(ArticleAddedArgs args)
    {
        dispatcher.Dispatch(new ArticleViewActions.LoadItem(args.VendorId, args.ArticleId, args.ArticleNumber));
    }

    private void OnArticleUpdated(ArticleUpdatedArgs args)
    {
        dispatcher.Dispatch(new ArticleViewActions.LoadItem(args.VendorId, args.ArticleId, args.ArticleNumber));
    }

    private void OnArticleStatusChanged(ArticleStatusChangedInfo args)
    {
        dispatcher.Dispatch(new ArticleViewActions.LoadItem(args.VendorId, args.ArticleId, args.ArticleNumber));
    }

    private void OnArticleRemoved(ArticleRemovedArgs args)
    {
        dispatcher.Dispatch(new ArticleViewActions.RemoveItem(args.VendorId, args.ArticleId, args.ArticleNumber));
    }
}