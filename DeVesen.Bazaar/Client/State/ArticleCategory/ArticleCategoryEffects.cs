using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryEffects(ArticleCategoryService articleCategoryService)
{
    [EffectMethod]
    public async Task FetchVendors(ArticleCategoryActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await articleCategoryService.GetAllAsync(action.Name, action.SearchText);

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new ArticleCategoryActions.FetchFailed());
        }

        var domainElements = response.Elements
            .OrderBy(p => p.Name)
            .ToArray();

        dispatcher.Dispatch(new ArticleCategoryActions.Set(domainElements));
    }
}