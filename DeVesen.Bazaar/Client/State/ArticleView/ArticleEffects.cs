using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public class ArticleEffects(ArticleService articleService)
{
    [EffectMethod]
    public async Task Fetch(ArticleActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await articleService.GetAllAsync(action.DealerId, action.Number, action.SearchText);

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new ArticleActions.FetchFailed());
        }

        var domainElements = response.Articles
            .OrderBy(p => p.Number)
            .ThenBy(p => p.Description);

        dispatcher.Dispatch(new ArticleActions.SetList(domainElements));
    }
}