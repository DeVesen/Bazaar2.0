using Fluxor;
using static DeVesen.Bazaar.Client.State.Settlement.SettlementState;

namespace DeVesen.Bazaar.Client.State.Settlement;

[FeatureState]
public record SettlementState(Models.VendorView? Vendor, IEnumerable<Models.Article> Articles, LoadingState State)
{
    public enum LoadingState
    {
        None,
        Loading,
        Failed,
        Loaded,
    }

    private SettlementState() : this(null, [], LoadingState.None) { }
}