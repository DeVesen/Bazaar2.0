namespace DeVesen.Bazaar.Shared.Events;

public record ArticleStatusChangedInfo(string VendorId, string ArticleId, long ArticleNumber, ArticleStatusChangedInfo.ChangedField Field)
{
    public enum ChangedField
    {
        Approved,
        Sold,
        Returned,
        Settled
    }
}