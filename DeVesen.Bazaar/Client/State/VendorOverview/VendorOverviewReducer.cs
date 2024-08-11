using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorOverview;

public static class VendorOverviewReducer
{
    [ReducerMethod(typeof(VendorOverviewActions.FetchVendors))]
    public static VendorOverviewState FetchVendors(VendorOverviewState state)
        => state with { Vendors = Enumerable.Empty<Models.VendorView>(), FilterData = state.FilterData, IsLoaded = false };

    [ReducerMethod]
    public static VendorOverviewState VendorsFetched(VendorOverviewState state, VendorOverviewActions.VendorsFetched action)
        => state with { Vendors = action.Vendors, FilterData = state.FilterData, IsLoaded = true };
}