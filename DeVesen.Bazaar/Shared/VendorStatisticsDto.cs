using DeVesen.Bazaar.Shared.Statistics;

namespace DeVesen.Bazaar.Shared;

public record VendorStatisticsDto
{
    public required string VendorId { get; init; }
    public required CountsStatistics Counts { get; init; }
    public required ValuesStatistics Values { get; init; }
}