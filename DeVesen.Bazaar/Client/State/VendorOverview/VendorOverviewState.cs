using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorOverview;

[FeatureState]
public record VendorOverviewState(IEnumerable<VendorView> Vendors, VendorFilter FilterData, bool IsLoaded)
{
    private VendorOverviewState() : this(Enumerable.Empty<VendorView>(), new VendorFilter(), false) { }
}