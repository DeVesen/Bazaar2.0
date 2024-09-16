using static DeVesen.Bazaar.Shared.Events.ArticleStatusChangedInfo;

namespace DeVesen.Bazaar.Shared.Events;

public record ArticleRemovedInfo(string VendorId, string ArticleId, long ArticleNumber);

public record ArticleStatusChangedInfo(string VendorId, string ArticleId, long ArticleNumber, ChangedField Field)
{
    public enum ChangedField
    {
        Approved,
        Sold,
        Returned,
        Settled
    }
}