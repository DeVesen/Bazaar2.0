using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Vendor;

public class VendorEffects
{
    private readonly VendorService _vendorService;

    public VendorEffects(VendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [EffectMethod]
    public async Task FetchVendors(VendorActions.FetchVendors action, IDispatcher dispatcher)
    {
        var elements = await _vendorService.GetAllAsync(action.Filter.Id, action.Filter.Salutation, action.Filter.SearchText);

        elements = elements.OrderBy(p => p.Item.LastName)
                           .ThenBy(p => p.Item.FirstName)
                           .ThenBy(p => p.Item.Salutation)
                           .ToArray();

        dispatcher.Dispatch(new VendorActions.VendorsFetched(elements));
    }
}