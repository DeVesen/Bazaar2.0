using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

public class ArticleEffects
{
    private readonly ArticleService _articleService;
    private readonly IState<ArticleState> _articleState;

    public ArticleEffects(ArticleService articleService, IState<ArticleState> articleState)
    {
        _articleService = articleService;
        _articleState = articleState;
    }

    [EffectMethod]
    public async Task FetchHazardSigns(ArticleActions.FetchArticles action, IDispatcher dispatcher)
    {
        var articles = await _articleService.GetAllAsync(action.Filter.VendorId, action.Filter.Number, action.Filter.SearchText);

        articles = articles.OrderBy(p => p.Number)
                           .ThenBy(p => p.Title);

        dispatcher.Dispatch(new ArticleActions.ArticlesFetched(articles));
    }
}