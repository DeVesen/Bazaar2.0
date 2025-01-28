using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

[FeatureState]
public record ArticleCategoryState(IEnumerable<Models.ArticleCategory> Elements, bool IsLoading)
{
    private ArticleCategoryState() : this([], false) { }
}