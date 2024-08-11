using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

public static class ArticleReducer
{
    [ReducerMethod(typeof(ArticleActions.FetchArticles))]
    public static ArticleState FetchArticles(ArticleState state)
        => state with { Articles = Enumerable.Empty<Models.Article>(), FilterData = state.FilterData, IsLoaded = false };

    [ReducerMethod]
    public static ArticleState ArticlesFetched(ArticleState state, ArticleActions.ArticlesFetched action)
    {
        return new(action.Articles, state.FilterData, true);
    }
}