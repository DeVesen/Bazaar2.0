using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

[FeatureState]
public record ArticleState(IEnumerable<Models.Article> Items, bool IsLoaded, string? ActualVendorId, bool BadVendor)
{
    private ArticleState() : this([], false, null, false) { }
}