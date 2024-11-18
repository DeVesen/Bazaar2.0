namespace DeVesen.Bazaar.Shared.Statistics;

public record CountsStatistics
{
    public required int Count { get; init; }
    public required int Approved { get; init; }
    public required int OnSale { get; init; }
    public required int Sold { get; init; }
    public required int Returned { get; init; }
    public required int Settled { get; init; }

    public static CountsStatistics Empty
        => new CountsStatistics
        {
            Count = 0,
            Approved = 0,
            OnSale = 0,
            Sold = 0,
            Returned = 0,
            Settled = 0,
        };
}