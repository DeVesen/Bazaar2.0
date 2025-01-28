using Fluxor;
using Mapster;
using SmartBazaar.Client.Models;
using SmartBazaar.Client.Services;

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
            .Select(p => p.Adapt<ArticleCategory>())
            .OrderBy(p => p.Title)
            .ToArray();

        dispatcher.Dispatch(new ArticleCategoryActions.SetAll(domainElements));
    }
}