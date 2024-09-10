using Fluxor;
using static DeVesen.Bazaar.Client.State.Settlement.SettlementState;

namespace DeVesen.Bazaar.Client.State.Settlement;

[FeatureState]
public record SettlementState(Models.VendorView? Vendor, Models.Article[] Articles, LoadingState State)
{
    public enum LoadingState
    {
        None,
        Loading,
        Failed,
        Loaded,
    }

    private SettlementState() : this(null, [], LoadingState.None) { }

    public bool IsEmpty
        => State == LoadingState.None;

    public bool IsLoading
        => State == LoadingState.Loading;

    public bool IsLoaded
        => State == LoadingState.Loaded;

    public bool IsFailed
        => State == LoadingState.Failed;

    public double GetOpenSales()
        => Articles.Where(p => p.IsSold() && p.IsSettled() is false)
                   .Sum(p => p.SoldAt ?? 0.0d);

    public double GetShareOfSales()
        => GetOpenSales() * (Vendor?.Item.SalesShare ?? 0.0d);

    public double GetArticleCommission()
        => Articles.Count(p => p.IsReturned() && p.IsSettled() is false) * (Vendor?.Item.OfferUnitPrice ?? 0.0d);
}