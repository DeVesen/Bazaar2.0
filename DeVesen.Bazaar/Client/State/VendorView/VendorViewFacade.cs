using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

public class VendorViewFacade(IDispatcher dispatcher)
{
    public void Fetch(string? id = null, string? salutation = null, string? searchText = null)
        => dispatcher.Dispatch(new VendorViewActions.Fetch(id, salutation, searchText));
}