
namespace DeVesen.Bazaar.Client.State.VendorView;

public class VendorViewActions
{
    public record Fetch(string? Id, string? Salutation, string? SearchText);

    public record Set(IEnumerable<Models.VendorView> Items);

    public record FetchFailed();
}