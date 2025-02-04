using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleState;

[FeatureState]
public record ArticleState(IEnumerable<ArticleModel> Elements, bool IsLoading)
{
    private ArticleState() : this([], false) { }
}