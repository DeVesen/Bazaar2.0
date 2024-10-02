using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

public class VendorViewEffects(VendorService vendorService)
{
    [EffectMethod]
    public async Task FetchVendors(VendorViewActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await vendorService.GetAllAsync(action.Id, action.SearchText);

        if (response.IsValid is false)
        {
            dispatcher.Dispatch(new VendorViewActions.FetchFailed());
        }

        dispatcher.Dispatch(new VendorViewActions.Set(response.Value));
    }
}