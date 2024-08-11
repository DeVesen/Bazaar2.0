using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryEffects
{
    private readonly ArticleCategoryService _articleCategoryService;

    public ArticleCategoryEffects(ArticleCategoryService articleCategoryService)
    {
        _articleCategoryService = articleCategoryService;
    }

    [EffectMethod]
    public async Task FetchVendors(ArticleCategoryActions.FetchArticleCategories action, IDispatcher dispatcher)
    {
        var elements = await _articleCategoryService.GetAllAsync(action.Filter);

        elements = elements.OrderBy(p => p.Name);

        dispatcher.Dispatch(new ArticleCategoryActions.ArticleCategoriesFetched(elements));
    }
}