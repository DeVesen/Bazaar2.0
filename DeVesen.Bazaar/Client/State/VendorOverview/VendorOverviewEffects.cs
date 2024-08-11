using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorOverview;

public class VendorOverviewEffects
{
    private readonly VendorService _vendorService;

    public VendorOverviewEffects(VendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [EffectMethod]
    public async Task FetchVendors(VendorOverviewActions.FetchVendors action, IDispatcher dispatcher)
    {
        var elements = await _vendorService.GetAllAsync(action.Filter.Id, action.Filter.Salutation, action.Filter.SearchText);

        elements = elements.OrderBy(p => p.Item.LastName)
                           .ThenBy(p => p.Item.FirstName)
                           .ThenBy(p => p.Item.Salutation);

        dispatcher.Dispatch(new VendorOverviewActions.VendorsFetched(elements));
    }
}