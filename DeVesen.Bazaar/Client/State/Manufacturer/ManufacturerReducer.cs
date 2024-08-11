using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public static class ManufacturerReducer
{
    [ReducerMethod(typeof(ManufacturerActions.FetchManufacturers))]
    public static ManufacturerState FetchVendors(ManufacturerState state)
        => state with { Vendors = Enumerable.Empty<Models.Manufacturer>(), FilterData = state.FilterData, IsLoaded = false };

    [ReducerMethod]
    public static ManufacturerState VendorsFetched(ManufacturerState state, ManufacturerActions.ManufacturersFetched action)
        => state with { Vendors = action.Vendors, FilterData = state.FilterData, IsLoaded = true };
}