namespace DeVesen.Bazaar.Server.Domain;

public record Article
{
    public required string Id { get; init; }
    public required string VendorId { get; init; }
    public required long Number { get; init; }

    public required string ArticleCategory { get; init; }
    public required string Manufacturer { get; init; }
    public required string Title { get; init; }

    public required DateTime Created { get; init; }
    public required double Price01 { get; init; }
    public double? Price02 { get; init; }

    public DateTime? ApprovedForSale { get; set; } = null;
    public DateTime? Sold { get; set; } = null;
    public double? SoldAt { get; set; } = null;
    public DateTime? Returned { get; set; } = null;
    public DateTime? Settled { get; set; } = null;
}