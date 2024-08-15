using Fluxor;

namespace DeVesen.Bazaar.Client.State.SalesCart;


[FeatureState]
public record SalesCartState(IEnumerable<PurchaseItem> PurchaseItems)
{
    private SalesCartState() : this(Enumerable.Empty<PurchaseItem>()) { }
}


public record PurchaseItem
{
    public required long ArticleNumber { get; init; }
    public required double SalesAmount { get; init; }
    public required Models.Article Article { get; init; }
}