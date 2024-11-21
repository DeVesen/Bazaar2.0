namespace DeVesen.Bazaar.Server.Infrastructure;

public record ArticleEntity
{
    public required string Id { get; set; }
    public required string VendorId { get; set; }
    public required long Number { get; set; }

    public required string ArticleCategory { get; set; }
    public required string Manufacturer { get; set; }
    public required string Description { get; set; }

    public required DateTime Created { get; set; }
    public required double Price01 { get; set; }
    public double? Price02 { get; set; }

    public DateTime? ApprovedForSale { get; set; } = null;
    public DateTime? Sold { get; set; } = null;
    public double? SoldAt { get; set; } = null;
    public DateTime? Returned { get; set; } = null;
    public DateTime? Settled { get; set; } = null;
}