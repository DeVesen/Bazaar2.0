using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

[FeatureState]
public record SettlementState(IEnumerable<object> Articles, bool IsLoaded)
{
    private SettlementState() : this(Enumerable.Empty<object>(), false) { }
}