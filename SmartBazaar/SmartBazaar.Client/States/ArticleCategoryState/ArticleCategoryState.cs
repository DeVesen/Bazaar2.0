using Fluxor;
using SmartBazaar.Client.Models;

namespace SmartBazaar.Client.States.ArticleCategoryState;

[FeatureState]
public record ArticleCategoryState(IEnumerable<ArticleCategory> Elements, bool IsLoading)
{
    private ArticleCategoryState() : this([], false) { }
}