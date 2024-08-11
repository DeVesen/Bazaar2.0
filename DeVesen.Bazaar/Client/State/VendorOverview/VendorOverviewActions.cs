using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.VendorOverview;

public class VendorOverviewActions
{
    public record FetchVendors(VendorFilter Filter);

    public record VendorsFetched(IEnumerable<VendorView> Vendors);
}