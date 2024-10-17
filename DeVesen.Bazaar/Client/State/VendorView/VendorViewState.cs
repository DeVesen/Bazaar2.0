using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

[FeatureState]
public record VendorViewState(IEnumerable<Models.VendorOverviewItem> Vendors, bool IsLoaded)
{
    private VendorViewState() : this([], false) { }
}