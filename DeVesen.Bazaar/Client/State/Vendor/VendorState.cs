using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Vendor;

[FeatureState]
public record VendorState(IEnumerable<VendorView> Vendors, VendorFilter FilterData, bool IsLoaded)
{
    private VendorState() : this(Enumerable.Empty<VendorView>(), new VendorFilter(), false) { }
}