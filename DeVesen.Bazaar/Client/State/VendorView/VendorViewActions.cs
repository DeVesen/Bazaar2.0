
namespace DeVesen.Bazaar.Client.State.VendorView;

public class VendorViewActions
{
    public record Fetch(string? Id, string? SearchText);

    public record Set(IEnumerable<Models.VendorOverviewItem> Items);

    public record FetchFailed();
}