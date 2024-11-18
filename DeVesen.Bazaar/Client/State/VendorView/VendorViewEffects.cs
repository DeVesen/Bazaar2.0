using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

public class VendorViewEffects(VendorService vendorService)
{
    [EffectMethod]
    public async Task FetchVendors(VendorViewActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await vendorService.GetOverviewAsync(action.Id, action.SearchText);

        if (response.IsValid is false)
        {
            dispatcher.Dispatch(new VendorViewActions.FetchFailed());
            return;
        }

        dispatcher.Dispatch(new VendorViewActions.SetList(response.Value));
    }

    [EffectMethod]
    public async Task LoadData(VendorViewActions.LoadData action, IDispatcher dispatcher)
    {
        if (action.MasterData)
        {
            var vendorResponse = await vendorService.GetByIdAsync(action.Id);

            if (vendorResponse.IsValid is false)
            {
                return;
            }

            dispatcher.Dispatch(new VendorViewActions.SetMasterData(vendorResponse.Value.Vendor));

            var statisticsResponse = await vendorService.GetStatisticsByVendor(action.Id);

            if (statisticsResponse.IsValid is false)
            {
                return;
            }

            dispatcher.Dispatch(new VendorViewActions.SetStatistic(action.Id, statisticsResponse.Value.Counts, statisticsResponse.Value.Values));
        }
        else if (action.StatisticData)
        {
            var statisticsResponse = await vendorService.GetStatisticsByVendor(action.Id);

            if (statisticsResponse.IsValid is false)
            {
                return;
            }

            dispatcher.Dispatch(new VendorViewActions.SetStatistic(action.Id, statisticsResponse.Value.Counts, statisticsResponse.Value.Values));
        }
    }
}