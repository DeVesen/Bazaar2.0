using DeVesen.Bazaar.Client.Models;
using Fluxor;
using static DeVesen.Bazaar.Client.State.Settlement.SettlementState;

namespace DeVesen.Bazaar.Client.State.Settlement;

[FeatureState]
public record SettlementState(Vendor? Vendor, VendorArticleStock? ArticleStock, VendorArticleValue? ArticleValue, Models.Article[] Articles, LoadingState State)
{
    public enum LoadingState
    {
        None,
        Loading,
        Failed,
        Loaded,
    }

    private SettlementState() : this(null, null, null, [], LoadingState.None) { }

    public bool IsEmpty
        => State == LoadingState.None;

    public bool IsLoading
        => State == LoadingState.Loading;

    public bool IsLoaded
        => State == LoadingState.Loaded;

    public bool IsFailed
        => State == LoadingState.Failed;
}