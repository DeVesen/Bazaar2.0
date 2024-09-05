using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

[FeatureState]
public record ArticleCategoryState(IEnumerable<Models.ArticleCategory> Items, bool IsLoaded)
{
    private ArticleCategoryState() : this([], false) { }
}