namespace DeVesen.Bazaar.Shared.Statistics;

public record ValuesStatistics
{
    public required double ApprovedValue { get; init; }
    public required double SoldValue { get; init; }
    public required double OfferValue { get; init; }
    public required double ShareValue { get; init; }

    public static ValuesStatistics Empty
        => new ValuesStatistics
        {
            ApprovedValue = 0,
            SoldValue = 0,
            OfferValue = 0,
            ShareValue = 0
        };
}