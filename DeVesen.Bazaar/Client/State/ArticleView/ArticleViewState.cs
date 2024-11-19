using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

[FeatureState]
public record ArticleViewState(IEnumerable<Models.Article> Items, bool IsLoaded, string? ActualVendorId, bool BadVendor)
{
    private ArticleViewState() : this([], false, null, false) { }
}