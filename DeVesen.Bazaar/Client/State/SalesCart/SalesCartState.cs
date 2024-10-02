using Fluxor;

namespace DeVesen.Bazaar.Client.State.SalesCart;


[FeatureState]
public record SalesCartState(IEnumerable<PurchaseItem> PurchaseItems, IEnumerable<CriticalWarning> CriticalWarnings, bool BookingInProcess)
{
    private SalesCartState() : this([], [], false) { }
}


public record PurchaseItem
{
    public required long ArticleNumber { get; init; }
    public required double SalesAmount { get; init; }
    public required Models.Article Article { get; init; }
}

public record CriticalWarning(long ArticleNumber, string Reason);