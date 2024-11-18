
using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared.Statistics;

namespace DeVesen.Bazaar.Client.State.VendorView;

public class VendorViewActions
{
    public record Fetch(string? Id, string? SearchText);

    public record CleanUp;

    public record SetMasterData(Vendor Vendor);

    public record SetStatistic(string Id, CountsStatistics Counts, ValuesStatistics Values);

    public record SetList(IEnumerable<VendorOverviewItem> Items);

    public record FetchFailed();

    public record LoadData(string Id, bool MasterData, bool StatisticData);

    public record Removed(string Id);
}