using Fluxor;

namespace DeVesen.Bazaar.Client.State.Article;

[FeatureState]
public record ArticleState(IEnumerable<Models.Article> Items, bool IsLoaded)
{
    private ArticleState() : this([], false) { }
}