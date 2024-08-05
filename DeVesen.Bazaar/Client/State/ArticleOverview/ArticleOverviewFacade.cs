using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleOverview;

public class ArticleOverviewFacade
{
    private readonly IDispatcher _dispatcher;

    public ArticleOverviewFacade(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void FetchArticles() => FetchArticles(new ArticleFilter());

    public void FetchArticles(ArticleFilter filter) => _dispatcher.Dispatch(new ArticleOverviewActions.FetchArticles(filter));
}