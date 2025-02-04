using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

[FeatureState]
public record DealerViewState(IEnumerable<Models.VendorOverviewItem> Vendors, bool IsLoaded)
{
    private DealerViewState() : this([], false) { }
}