using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

public class ArticleFacade(IDispatcher dispatcher)
{
    public void Fetch(string? vendorId = null, string? number = null, string? searchText = null)
        => dispatcher.Dispatch(new ArticleActions.Fetch(vendorId, number, searchText));
}