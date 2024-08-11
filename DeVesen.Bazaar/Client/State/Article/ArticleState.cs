using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

[FeatureState]
public record ArticleState(IEnumerable<Models.Article> Articles, ArticleFilter FilterData, bool IsLoaded)
{
    private ArticleState() : this(Enumerable.Empty<Models.Article>(), new ArticleFilter(), false) { }
}