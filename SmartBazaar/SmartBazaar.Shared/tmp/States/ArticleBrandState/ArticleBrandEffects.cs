using Fluxor;
using Mapster;
using SmartBazaar.Client.Models;
using SmartBazaar.Client.Services;

namespace SmartBazaar.Client.States.ArticleBrandState;

public class ArticleBrandEffects(ArticleBrandHttpService articleBrandService)
{
    [EffectMethod]
    public async Task Fetch(ArticleBrandActions.FetchAll action, IDispatcher dispatcher)
    {
        var response = await articleBrandService.GetAllAsync();

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new ArticleBrandActions.FetchAllFailed());
        }

        var domainElements = response.Elements
            .Select(p => p.Adapt<ArticleBrand>())
            .OrderBy(p => p.Title)
            .ToArray();

        dispatcher.Dispatch(new ArticleBrandActions.SetAll(domainElements));
    }
}