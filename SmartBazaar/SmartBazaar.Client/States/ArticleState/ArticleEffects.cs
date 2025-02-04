using Fluxor;
using SmartBazaar.Shared.Contracts;

namespace SmartBazaar.Client.States.ArticleState;

public class ArticleEffects(IArticleService articleService)
{
    [EffectMethod]
    public async Task Fetch(ArticleActions.FetchAll action, IDispatcher dispatcher)
    {
        var response = await articleService.GetAllAsync();

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new ArticleActions.FetchAllFailed());
        }
        var domainElements = response.Value
            .OrderBy(p => p.Number)
            .ToArray();

        dispatcher.Dispatch(new ArticleActions.SetAll(domainElements));
    }
}