using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.Article;

public class ArticleActions
{
    public record FetchArticles(ArticleFilter Filter);

    public record ArticlesFetched(IEnumerable<Models.Article> Articles);
}