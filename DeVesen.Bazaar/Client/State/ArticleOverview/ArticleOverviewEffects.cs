using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleOverview;

public class ArticleOverviewEffects
{
    private readonly ArticleService _articleService;
    private readonly IState<ArticleOverviewState> _articleState;

    public ArticleOverviewEffects(ArticleService articleService, IState<ArticleOverviewState> articleState)
    {
        _articleService = articleService;
        _articleState = articleState;
    }

    [EffectMethod]
    public async Task FetchHazardSigns(ArticleOverviewActions.FetchArticles action, IDispatcher dispatcher)
    {
        var articles = await _articleService.GetAllAsync(action.Filter.VendorId, action.Filter.Number, action.Filter.SearchText);

        articles = articles.OrderBy(p => p.Number)
                           .ThenBy(p => p.Title);

        dispatcher.Dispatch(new ArticleOverviewActions.ArticlesFetched(articles));
    }
}