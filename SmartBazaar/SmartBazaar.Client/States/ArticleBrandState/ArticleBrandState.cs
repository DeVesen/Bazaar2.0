using Fluxor;
using SmartBazaar.Client.Models;

namespace SmartBazaar.Client.States.ArticleBrandState;

[FeatureState]
public record ArticleBrandState(IEnumerable<ArticleBrand> Elements, bool IsLoading)
{
    private ArticleBrandState() : this([], false) { }
}