using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

public class VendorViewEffects(VendorService vendorService)
{
    [EffectMethod]
    public async Task FetchVendors(VendorViewActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await vendorService.GetOverviewAsync(action.Id, action.SearchText);

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new VendorViewActions.FetchFailed());
            return;
        }

        dispatcher.Dispatch(new VendorViewActions.SetList(response.Vendors));
    }

    [EffectMethod]
    public async Task LoadData(VendorViewActions.LoadData action, IDispatcher dispatcher)
    {
        if (action.MasterData)
        {
            var vendorResponse = await vendorService.GetByIdAsync(action.Id);

            if (vendorResponse.Found is false)
            {
                return;
            }

            dispatcher.Dispatch(new VendorViewActions.SetMasterData(vendorResponse.Vendor));

            var statisticsResponse = await vendorService.GetStatisticsByVendor(action.Id);

            if (statisticsResponse.IsSuccess is false)
            {
                return;
            }

            dispatcher.Dispatch(new VendorViewActions.SetStatistic(action.Id, statisticsResponse.Counts, statisticsResponse.Values));
        }
        else if (action.StatisticData)
        {
            var statisticsResponse = await vendorService.GetStatisticsByVendor(action.Id);

            if (statisticsResponse.IsSuccess is false)
            {
                return;
            }

            dispatcher.Dispatch(new VendorViewActions.SetStatistic(action.Id, statisticsResponse.Counts, statisticsResponse.Values));
        }
    }
}