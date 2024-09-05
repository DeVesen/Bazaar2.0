using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public static class ManufacturerReducer
{
    [ReducerMethod(typeof(ManufacturerActions.Fetch))]
    public static ManufacturerState FetchVendors(ManufacturerState state)
        => state with { Items = Enumerable.Empty<Models.Manufacturer>(), IsLoaded = false };

    [ReducerMethod]
    public static ManufacturerState VendorsFetched(ManufacturerState state, ManufacturerActions.Set action)
        => state with { Items = action.Items, IsLoaded = true };

    [ReducerMethod(typeof(ManufacturerActions.FetchFailed))]
    public static ManufacturerState VendorsFetched(ManufacturerState state)
        => state with { IsLoaded = true };
}