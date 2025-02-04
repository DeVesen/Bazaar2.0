using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleCategoryState;

[FeatureState]
public record ArticleCategoryState(IEnumerable<ArticleCategoryModel> Elements, bool IsLoading)
{
    private ArticleCategoryState() : this([], false) { }
}