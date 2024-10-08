﻿using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record VendorFilterDto
{
    public string? Id { get; init; } = null;
    public string? SearchText { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record VendorDto
{
    public required string Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Address { get; init; }
    public string? EMail { get; init; }
    public string? Phone { get; init; }
    public string? Note { get; init; }
    public double OfferUnitPrice { get; set; }
    public double SalesShare { get; set; }
}

[ExcludeFromCodeCoverage]
public record VendorViewDto
{
    public required VendorDto Item { get; init; }
    public required VendorArticleStatisticDto Statistic { get; init; }
}

[ExcludeFromCodeCoverage]
public record VendorCreateDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Address { get; init; }
    public string? EMail { get; init; }
    public string? Phone { get; init; }
    public string? Note { get; init; }
    public double OfferUnitPrice { get; set; }
    public double SalesShare { get; set; }
}

[ExcludeFromCodeCoverage]
public record VendorUpdateDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Address { get; init; }
    public string? EMail { get; init; }
    public string? Phone { get; init; }
    public string? Note { get; init; }
    public double OfferUnitPrice { get; set; }
    public double SalesShare { get; set; }
}

[ExcludeFromCodeCoverage]
public record VendorCreatedDto
{
    public required string Id { get; init; }
}

[ExcludeFromCodeCoverage]
public record VendorArticleStatisticDto
{
    public long NotOpen { get; set; } = 0;
    public long Open { get; init; } = 0;
    public long Sold { get; init; } = 0;
    public long Settled { get; init; } = 0;
    public double Turnover { get; init; } = 0;
}