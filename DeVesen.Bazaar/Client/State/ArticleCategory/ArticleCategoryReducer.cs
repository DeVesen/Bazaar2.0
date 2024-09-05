using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public static class ArticleCategoryReducer
{
    [ReducerMethod(typeof(ArticleCategoryActions.Fetch))]
    public static ArticleCategoryState FetchVendors(ArticleCategoryState state)
        => state with { Items = [], IsLoaded = false };

    [ReducerMethod]
    public static ArticleCategoryState VendorsFetched(ArticleCategoryState state, ArticleCategoryActions.Set action)
        => state with { Items = action.Items, IsLoaded = true };

    [ReducerMethod(typeof(ArticleCategoryActions.FetchFailed))]
    public static ArticleCategoryState VendorsFetched(ArticleCategoryState state)
        => state with { IsLoaded = true };
}