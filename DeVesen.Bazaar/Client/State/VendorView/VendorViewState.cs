using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

[FeatureState]
public record VendorViewState(IEnumerable<Models.VendorView> Vendors, bool IsLoaded)
{
    private VendorViewState() : this(Enumerable.Empty<Models.VendorView>(), false) { }
}