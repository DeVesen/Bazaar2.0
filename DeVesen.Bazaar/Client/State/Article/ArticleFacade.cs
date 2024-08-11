using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

public class ArticleFacade
{
    private readonly IDispatcher _dispatcher;

    public ArticleFacade(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void Fetch() => Fetch(new ArticleFilter());

    public void Fetch(ArticleFilter filter) => _dispatcher.Dispatch(new ArticleActions.FetchArticles(filter));
}