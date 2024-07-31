namespace DeVesen.Bazaar.Server.Domain;

public record Article
{
    public required string Id { get; init; }
    public required string VendorId { get; init; }
    public required long Number { get; init; }
    public required string Title { get; init; }
    public required string ArticleCategory { get; init; }
    public required string Manufacturer { get; init; }
    public required DateTime Created { get; init; }
    public required double Price01 { get; init; }
    public double? Price02 { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? ApprovedForSale { get; init; } = null;
    public DateTime? Sold { get; init; } = null;
    public double? SoldAt { get; init; } = null;
    public DateTime? Settled { get; init; } = null;
}