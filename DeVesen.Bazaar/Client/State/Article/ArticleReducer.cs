using DeVesen.Bazaar.Client.State.Article;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

public static class ArticleReducer
{
    [ReducerMethod(typeof(ArticleActions.Fetch))]
    public static ArticleState FetchVendors(ArticleState state)
        => state with { Items = [], IsLoaded = false };

    [ReducerMethod]
    public static ArticleState VendorsFetched(ArticleState state, ArticleActions.Set action)
        => state with { Items = action.Items, IsLoaded = true };

    [ReducerMethod(typeof(ArticleActions.FetchFailed))]
    public static ArticleState VendorsFetched(ArticleState state)
        => state with { IsLoaded = true };
}