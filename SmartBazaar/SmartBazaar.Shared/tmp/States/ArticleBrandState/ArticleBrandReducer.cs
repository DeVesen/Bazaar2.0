using Fluxor;

namespace SmartBazaar.Client.States.ArticleBrandState;

public static class ArticleBrandReducer
{
    [ReducerMethod(typeof(ArticleBrandActions.FetchAll))]
    public static ArticleBrandState FetchAll(ArticleBrandState state)
        => state with { Elements = [], IsLoading = true };

    [ReducerMethod]
    public static ArticleBrandState SetAll(ArticleBrandState state, ArticleBrandActions.SetAll action)
        => state with { Elements = action.Elements, IsLoading = false };

    [ReducerMethod(typeof(ArticleBrandActions.FetchAllFailed))]
    public static ArticleBrandState FetchAllFailed(ArticleBrandState state)
        => state with { IsLoading = false };
}