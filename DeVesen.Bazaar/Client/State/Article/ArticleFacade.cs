using DeVesen.Bazaar.Client.Domain;
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

public class ArticleFacade
{
    private readonly IDispatcher _dispatcher;
    private readonly ArticleService _articleService;

    public ArticleFacade(IDispatcher dispatcher, ArticleService articleService)
    {
        _dispatcher = dispatcher;
        _articleService = articleService;
    }

    public async Task<Response> ApproveArticleAsync(long articleNumber)
        => await _articleService.ApproveAsync(articleNumber);

    public void Fetch() => Fetch(new ArticleFilter());

    public void Fetch(ArticleFilter filter) => _dispatcher.Dispatch(new ArticleActions.FetchArticles(filter));
}