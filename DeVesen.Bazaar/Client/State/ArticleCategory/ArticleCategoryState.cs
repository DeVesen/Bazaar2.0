using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

[FeatureState]
public record ArticleCategoryState(IEnumerable<Models.ArticleCategory> Items, ArticleCategoryFilter FilterData, bool IsLoaded)
{
    private ArticleCategoryState() : this(Enumerable.Empty<Models.ArticleCategory>(), new ArticleCategoryFilter(), false) { }
}