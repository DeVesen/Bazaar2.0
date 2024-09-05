using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

public static class VendorViewReducer
{
    [ReducerMethod(typeof(VendorViewActions.Fetch))]
    public static VendorViewState FetchVendors(VendorViewState state)
        => state with { Vendors = [], IsLoaded = false };

    [ReducerMethod]
    public static VendorViewState VendorsFetched(VendorViewState state, VendorViewActions.Set action)
        => state with { Vendors = action.Items, IsLoaded = true };
}