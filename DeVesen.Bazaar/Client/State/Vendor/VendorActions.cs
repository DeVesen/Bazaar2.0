using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.Vendor;

public class VendorActions
{
    public record FetchVendors(VendorFilter Filter);

    public record VendorsFetched(IEnumerable<VendorView> Vendors);
}