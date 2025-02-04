using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.DealerInfoState;

[FeatureState]
public record DealerInfoState(IEnumerable<DealerInfoAggregate> Elements, bool IsLoading)
{
    private DealerInfoState() : this([], false) { }
}