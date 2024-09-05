namespace DeVesen.Bazaar.Client.State.Settlement;

public class SettlementActions
{
    public record FetchSettlement(string VendorId);

    public record SetSettlement(IEnumerable<object> Articles);

    public record SettlementFetchFailed;
}