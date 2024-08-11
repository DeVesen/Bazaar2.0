using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleOverview;

[FeatureState]
public record ArticleOverviewState(IEnumerable<Article> Articles, ArticleFilter FilterData, bool IsLoaded)
{
    private ArticleOverviewState() : this(Enumerable.Empty<Article>(), new ArticleFilter(), false) { }
}