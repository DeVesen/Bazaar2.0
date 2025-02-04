using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Shared.Events;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

public class DealerViewFacade(IDispatcher dispatcher, VendorHubConnectionService vendorHub)
{
    public void Fetch(string? id = null, string? searchText = null)
        => dispatcher.Dispatch(new DealerViewActions.Fetch(id, searchText));

    public void CleanUp()
        => dispatcher.Dispatch(new DealerViewActions.CleanUp());


    public async Task StartCallbacks()
    {
        if (vendorHub.IsWorking())
        {
            return;
        }

        await vendorHub.StartAsync();

        vendorHub.RegisterOnAdded(OnAdded);
        vendorHub.RegisterOnUpdated(OnUpdated);
        vendorHub.RegisterOnRemoved(OnRemoved);
    }

    public async Task StopCallbacks()
    {
        await vendorHub.StopAsync();
    }


    private void OnAdded(VendorAddedArgs args)
    {
        dispatcher.Dispatch(new DealerViewActions.LoadData(args.Id, true, true));
    }

    private void OnUpdated(VendorUpdatedArgs args)
    {
        var masterData = args.Reason == VendorUpdatedArgs.Reasons.MasterData;
        var statisticData = args.Reason == VendorUpdatedArgs.Reasons.ArticleData;

        dispatcher.Dispatch(new DealerViewActions.LoadData(args.Id, masterData, statisticData));
    }

    private void OnRemoved(VendorRemovedArgs args)
    {
        dispatcher.Dispatch(new DealerViewActions.Removed(args.Id));
    }
}