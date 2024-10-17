namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementActions
{
    public record FetchSettlement(string VendorId);

    public record SetSettlement
    {
        public required Models.Vendor Vendor { get; set; }
        public required Models.VendorArticleStock ArticleStock { get; set; }
        public required Models.VendorArticleValue ArticleValue { get; set; }
        public required Models.Article[] Articles { get; set; }
    }

    public record SettlementFetchFailed;

    public record PayOut(string VendorId);

    public record ResetSelection;
}