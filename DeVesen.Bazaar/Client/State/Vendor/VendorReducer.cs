using Fluxor;

namespace DeVesen.Bazaar.Client.State.Vendor;

public static class VendorReducer
{
    [ReducerMethod(typeof(VendorActions.FetchVendors))]
    public static VendorState FetchVendors(VendorState state)
        => state with { Vendors = Enumerable.Empty<Models.VendorView>(), FilterData = state.FilterData, IsLoaded = false };

    [ReducerMethod]
    public static VendorState VendorsFetched(VendorState state, VendorActions.VendorsFetched action)
        => state with { Vendors = action.Vendors, FilterData = state.FilterData, IsLoaded = true };
}