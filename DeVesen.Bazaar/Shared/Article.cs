using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record ArticleFilterDto
{
    public string? VendorId { get; set; } = null;
    public long? Number { get; set; } = null;
    public string? Title { get; set; } = null;
    public string? ArticleCategory { get; set; } = null;
    public string? Manufacturer { get; set; } = null;
}

[ExcludeFromCodeCoverage]
public record ArticleDto
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
    public string? Description { get; init; }
    public DateTime? ApprovedForSale { get; init; } = null;
    public DateTime? Sold { get; init; } = null;
    public double? SoldAt { get; init; } = null;
    public DateTime? Settled { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record ArticleCreateDto
{
    public required string VendorId { get; init; }
    public required long Number { get; init; }
    public required string Title { get; init; }
    public required string ArticleCategory { get; init; }
    public required string Manufacturer { get; init; }
    public required double Price01 { get; init; }
    public double? Price02 { get; init; } = null;
    public string? Description { get; init; }
    public DateTime? ApprovedForSale { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record ArticleUpdateDto
{
    public required string VendorId { get; init; }
    public required long Number { get; init; }
    public required string Title { get; init; }
    public required string ArticleCategory { get; init; }
    public required string Manufacturer { get; init; }
    public required DateTime Created { get; init; }
    public required double Price01 { get; init; }
    public double? Price02 { get; init; } = null;
    public string? Description { get; init; }
    public DateTime? ApprovedForSale { get; init; } = null;
    public DateTime? Sold { get; init; } = null;
    public double? SoldAt { get; init; } = null;
    public DateTime? Settled { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record ArticleCreatedDto
{
    public required string Id { get; init; }
}