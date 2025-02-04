using Fluxor;
using SmartBazaar.Shared.Contracts;

namespace SmartBazaar.Client.States.ArticleBrandState;

public class ArticleBrandEffects(IArticleBrandService articleBrandService)
{
    [EffectMethod]
    public async Task Fetch(ArticleBrandActions.FetchAll action, IDispatcher dispatcher)
    {
        var response = await articleBrandService.GetAllAsync();

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new ArticleBrandActions.FetchAllFailed());
        }

        var domainElements = response.Value
            .OrderBy(p => p.Title)
            .ToArray();

        dispatcher.Dispatch(new ArticleBrandActions.SetAll(domainElements));
    }
}