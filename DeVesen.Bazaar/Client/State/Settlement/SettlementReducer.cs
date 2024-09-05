using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public static class SettlementReducer
{
    [ReducerMethod(typeof(SettlementActions.FetchSettlement))]
    public static SettlementState FetchSettlement(SettlementState state)
        => state with { Articles = Enumerable.Empty<Models.Article>(), State = SettlementState.LoadingState.Loading };

    [ReducerMethod]
    public static SettlementState SettlementFetched(SettlementState state, SettlementActions.SetSettlement action)
        => state with { Vendor = action.VendorView, Articles = action.Articles, State = SettlementState.LoadingState.Loaded };

    [ReducerMethod(typeof(SettlementActions.SettlementFetchFailed))]
    public static SettlementState SettlementFetchFailed(SettlementState state)
        => state with { State = SettlementState.LoadingState.Failed };
}