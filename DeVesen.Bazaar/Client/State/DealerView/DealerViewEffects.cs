using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

public class DealerViewEffects(VendorService vendorService)
{
    [EffectMethod]
    public async Task FetchVendors(DealerViewActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await vendorService.GetOverviewAsync(action.Id, action.SearchText);

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new DealerViewActions.FetchFailed());
            return;
        }

        dispatcher.Dispatch(new DealerViewActions.SetList(response.Vendors));
    }

    [EffectMethod]
    public async Task LoadData(DealerViewActions.LoadData action, IDispatcher dispatcher)
    {
        if (action.MasterData)
        {
            var vendorResponse = await vendorService.GetByIdAsync(action.Id);

            if (vendorResponse.Found is false)
            {
                return;
            }

            dispatcher.Dispatch(new DealerViewActions.SetMasterData(vendorResponse.Vendor));

            var statisticsResponse = await vendorService.GetStatisticsByVendor(action.Id);

            if (statisticsResponse.IsSuccess is false)
            {
                return;
            }

            dispatcher.Dispatch(new DealerViewActions.SetStatistic(action.Id, statisticsResponse.Counts, statisticsResponse.Values));
        }
        else if (action.StatisticData)
        {
            var statisticsResponse = await vendorService.GetStatisticsByVendor(action.Id);

            if (statisticsResponse.IsSuccess is false)
            {
                return;
            }

            dispatcher.Dispatch(new DealerViewActions.SetStatistic(action.Id, statisticsResponse.Counts, statisticsResponse.Values));
        }
    }
}