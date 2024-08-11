using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorOverview;

public class VendorOverviewFacade
{
    private readonly IDispatcher _dispatcher;

    public VendorOverviewFacade(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void FetchVendors() => FetchVendors(new VendorFilter());

    public void FetchVendors(VendorFilter filter) => _dispatcher.Dispatch(new VendorOverviewActions.FetchVendors(filter));
}