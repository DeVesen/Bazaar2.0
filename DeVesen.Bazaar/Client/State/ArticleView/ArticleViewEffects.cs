using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public class ArticleViewEffects(ArticleService articleService)
{
    [EffectMethod]
    public async Task Fetch(ArticleViewActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await articleService.GetAllAsync(action.VendorId, action.Number, action.SearchText);

        if (response.IsValid is false)
        {
            dispatcher.Dispatch(new ArticleViewActions.FetchFailed());
        }

        var domainElements = response.Value
            .OrderBy(p => p.Number)
            .ThenBy(p => p.Description);

        dispatcher.Dispatch(new ArticleViewActions.SetList(domainElements));
    }
}