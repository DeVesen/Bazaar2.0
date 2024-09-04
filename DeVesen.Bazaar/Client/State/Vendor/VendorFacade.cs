using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Vendor;

public class VendorFacade
{
    private readonly VendorState _vendorState;
    private readonly VendorService _vendorService;
    private readonly IDispatcher _dispatcher;

    public VendorFacade(IState<VendorState> vendorState,
                        VendorService vendorService,
                        IDispatcher dispatcher)
    {
        _vendorState = vendorState.Value;
        _vendorService = vendorService;
        _dispatcher = dispatcher;
    }

    public async Task<Models.Vendor?> GetById(string id) => (await _vendorService.GetAsync(id))?.Item;

    public bool TryGetViewById(string id, out VendorView? item)
    {
        item = _vendorState.Vendors.FirstOrDefault(p => p.Item.Id == id);
        return item != null;
    }

    public void Fetch() => Fetch(new VendorFilter());

    public void Fetch(VendorFilter filter) => _dispatcher.Dispatch(new VendorActions.FetchVendors(filter));
}