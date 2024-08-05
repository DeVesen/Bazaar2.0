using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.ArticleOverview;

public class ArticleOverviewActions
{
    public record FetchArticles(ArticleFilter Filter);

    public record ArticlesFetched(IEnumerable<Article> Articles);
}