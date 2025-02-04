using Fluxor;
using SmartBazaar.Shared.Contracts;

namespace SmartBazaar.Client.States.ArticleCategoryState;

public class ArticleCategoryEffects(IArticleCategoryService articleCategoryService)
{
    [EffectMethod]
    public async Task Fetch(ArticleCategoryActions.FetchAll action, IDispatcher dispatcher)
    {
        var response = await articleCategoryService.GetAllAsync();

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new ArticleCategoryActions.FetchAllFailed());
        }

        var domainElements = response.Value
            .OrderBy(p => p.Title)
            .ToArray();

        dispatcher.Dispatch(new ArticleCategoryActions.SetAll(domainElements));
    }
}