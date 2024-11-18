using DeVesen.Bazaar.Shared.Statistics;

namespace DeVesen.Bazaar.Client.Models;

public record VendorOverviewItem
{
    public required Vendor Vendor { get; set; }
    public required CountsStatistics Counts { get; set; }
    public required ValuesStatistics Values { get; set; }
}