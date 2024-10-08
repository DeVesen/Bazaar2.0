﻿using System.Diagnostics.CodeAnalysis;
using static DeVesen.Bazaar.Shared.SalesOrderDto;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record ArticleFilterDto
{
    public string? VendorId { get; init; } = null;
    public string? Number { get; init; } = null;
    public string? SearchText { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record ArticleDto
{
    public required string Id { get; init; }
    public required string VendorId { get; init; }
    public required long Number { get; init; }

    public required string ArticleCategory { get; init; }
    public required string Manufacturer { get; init; }
    public required string Description { get; init; }

    public required DateTime Created { get; init; }
    public required double Price01 { get; init; }
    public double? Price02 { get; init; }

    public DateTime? ApprovedForSale { get; init; } = null;
    public DateTime? Sold { get; init; } = null;
    public double? SoldAt { get; init; } = null;
    public DateTime? Returned { get; set; }
    public DateTime? Settled { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record ArticleCreateDto
{
    public required string VendorId { get; init; }
    public required long Number { get; init; }
    public required string ArticleCategory { get; init; }
    public required string Manufacturer { get; init; }
    public required string Description { get; init; }
    public required double Price01 { get; init; }
    public double? Price02 { get; init; } = null;
    public DateTime? ApprovedForSale { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record ArticleUpdateDto
{
    public required string VendorId { get; init; }
    public required long Number { get; init; }
    public required string ArticleCategory { get; init; }
    public required string Manufacturer { get; init; }
    public required string Description { get; init; }
    public required DateTime Created { get; init; }
    public required double Price01 { get; init; }
    public double? Price02 { get; init; } = null;
    public DateTime? ApprovedForSale { get; init; } = null;
    public DateTime? Sold { get; init; } = null;
    public double? SoldAt { get; init; } = null;
    public DateTime? Returned { get; set; }
    public DateTime? Settled { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record ArticleCreatedDto
{
    public required string Id { get; init; }
}

[ExcludeFromCodeCoverage]
public record SalesOrderDto(IEnumerable<Position> Positions)
{
    public record Position(long Number, double Price);
}

[ExcludeFromCodeCoverage]
public record NextArticleNumberDto(long Number);