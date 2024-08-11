using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Vendor;

public class VendorFacade
{
    private readonly VendorState _vendorState;
    private readonly IDispatcher _dispatcher;

    public VendorFacade(IState<VendorState> vendorState, IDispatcher dispatcher)
    {
        _vendorState = vendorState.Value;
        _dispatcher = dispatcher;
    }

    public Models.Vendor? GetById(string id) => GetViewById(id)?.Item;

    public VendorView? GetViewById(string id) => _vendorState.Vendors.FirstOrDefault(p => p.Item.Id == id);

    public void Fetch() => Fetch(new VendorFilter());

    public void Fetch(VendorFilter filter) => _dispatcher.Dispatch(new VendorActions.FetchVendors(filter));
}