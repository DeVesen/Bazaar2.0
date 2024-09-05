using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryFacade(IDispatcher dispatcher)
{
    public void Fetch(string? name = null, string? searchText = null)
        => dispatcher.Dispatch(new ArticleCategoryActions.Fetch(name, searchText));
}