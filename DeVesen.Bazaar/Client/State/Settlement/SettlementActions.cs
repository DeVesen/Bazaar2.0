namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementActions
{
    public record FetchSettlement(string VendorId);

    public record SetSettlement(Models.VendorView VendorView, Models.Article[] Articles);

    public record SettlementFetchFailed;

    public record PayOut(string VendorId, IEnumerable<string> ArticleIds);

    public record ResetSelection;
}