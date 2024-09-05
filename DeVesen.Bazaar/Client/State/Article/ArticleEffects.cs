using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

public class ArticleEffects(ArticleService articleService)
{
    [EffectMethod]
    public async Task Fetch(ArticleActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await articleService.GetAllAsync(action.VendorId, action.Number, action.SearchText);

        if (response.IsValid is false)
        {
            dispatcher.Dispatch(new ArticleActions.FetchFailed());
        }

        var domainElements = response.Value
            .OrderBy(p => p.Number)
            .ThenBy(p => p.Title);

        dispatcher.Dispatch(new ArticleActions.Set(domainElements));
    }
}