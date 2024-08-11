using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleOverview;

public static class ArticleOverviewReducer
{
    [ReducerMethod(typeof(ArticleOverviewActions.FetchArticles))]
    public static ArticleOverviewState FetchArticles(ArticleOverviewState state)
        => state with { Articles = Enumerable.Empty<Models.Article>(), FilterData = state.FilterData, IsLoaded = false };

    [ReducerMethod]
    public static ArticleOverviewState ArticlesFetched(ArticleOverviewState state, ArticleOverviewActions.ArticlesFetched action)
    {
        return new(action.Articles, state.FilterData, true);
    }
}