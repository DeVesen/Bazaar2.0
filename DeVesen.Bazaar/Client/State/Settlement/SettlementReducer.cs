using Fluxor;

namespace DeVesen.Bazaar.Client.State.Settlement;

public static class SettlementReducer
{
    [ReducerMethod(typeof(SettlementActions.FetchSettlement))]
    public static SettlementState FetchSettlement(SettlementState state)
        => state with { Vendor = null, Articles = [], State = SettlementState.LoadingState.Loading };

    [ReducerMethod]
    public static SettlementState SettlementFetched(SettlementState state, SettlementActions.SetSettlement action)
        => state with
        {
            Vendor = action.Vendor,
            ArticleStock = action.ArticleStock,
            ArticleValue = action.ArticleValue,
            Articles = action.Articles,
            State = SettlementState.LoadingState.Loaded
        };

    [ReducerMethod(typeof(SettlementActions.SettlementFetchFailed))]
    public static SettlementState SettlementFetchFailed(SettlementState state)
        => state with { State = SettlementState.LoadingState.Failed };

    [ReducerMethod(typeof(SettlementActions.ResetSelection))]
    public static SettlementState ResetSelection(SettlementState state)
        => state with { Vendor = null, Articles = [], State = SettlementState.LoadingState.None };
}