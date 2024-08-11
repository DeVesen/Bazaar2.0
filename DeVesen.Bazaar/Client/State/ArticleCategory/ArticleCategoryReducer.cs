using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public static class ArticleCategoryReducer
{
    [ReducerMethod(typeof(ArticleCategoryActions.FetchArticleCategories))]
    public static ArticleCategoryState FetchVendors(ArticleCategoryState state)
        => state with { Items = Enumerable.Empty<Models.ArticleCategory>(), FilterData = state.FilterData, IsLoaded = false };

    [ReducerMethod]
    public static ArticleCategoryState VendorsFetched(ArticleCategoryState state, ArticleCategoryActions.ArticleCategoriesFetched action)
        => state with { Items = action.Items, FilterData = state.FilterData, IsLoaded = true };
}