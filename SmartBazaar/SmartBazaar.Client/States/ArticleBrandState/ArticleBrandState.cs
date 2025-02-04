using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleBrandState;

[FeatureState]
public record ArticleBrandState(IEnumerable<ArticleBrandModel> Elements, bool IsLoading)
{
    private ArticleBrandState() : this([], false) { }
}