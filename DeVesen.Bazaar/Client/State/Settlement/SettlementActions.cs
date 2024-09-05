namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementActions
{
    public record FetchSettlement(string VendorId);

    public record SetSettlement(Models.VendorView VendorView, IEnumerable<Models.Article> Articles);

    public record SettlementFetchFailed;
}