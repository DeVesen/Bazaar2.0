using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public static class SettlementReducer
{
    [ReducerMethod(typeof(SettlementActions.FetchSettlement))]
    public static SettlementState FetchSettlement(SettlementState state)
        => state with { Articles = Enumerable.Empty<Models.Article>(), IsLoaded = false };

    [ReducerMethod]
    public static SettlementState SettlementFetched(SettlementState state, SettlementActions.SetSettlement action)
        => state with { Articles = action.Articles, IsLoaded = true };

    [ReducerMethod(typeof(SettlementActions.SettlementFetchFailed))]
    public static SettlementState SettlementFetchFailed(SettlementState state)
        => state with { IsLoaded = true };
}