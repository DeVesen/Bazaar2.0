using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryFacade
{
    private readonly ArticleCategoryState _articleCategoryState;
    private readonly IDispatcher _dispatcher;

    public ArticleCategoryFacade(IState<ArticleCategoryState> articleCategoryState, IDispatcher dispatcher)
    {
        _articleCategoryState = articleCategoryState.Value;
        _dispatcher = dispatcher;
    }

    public IEnumerable<Models.ArticleCategory> GetArticleCategories() => _articleCategoryState.Items;

    public void Fetch() => Fetch(new ArticleCategoryFilter());

    public void Fetch(ArticleCategoryFilter filter) => _dispatcher.Dispatch(new ArticleCategoryActions.FetchArticleCategories(filter));
}