using System.Diagnostics.CodeAnalysis;

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
public record VendorOverviewItemDto
{
    public required VendorDto Vendor { get; init; }
    public required VendorArticleStockDto ArticleStock { get; init; }
}

[ExcludeFromCodeCoverage]
public record VendorSettlementDto
{
    public required VendorDto Vendor { get; init; }
    public required VendorArticleStockDto ArticleStock { get; init; }
    public required VendorArticleValueDto ArticleValue { get; init; }
    public required IEnumerable<ArticleDto> Articles { get; set; }
}

[ExcludeFromCodeCoverage]
public record VendorArticleStockDto
{
    public long Recorded { get; set; } = 0;
    public long OnSale { get; init; } = 0;
    public long Sold { get; init; } = 0;
    public long Returned { get; init; } = 0;
    public long Settled { get; init; } = 0;
}

[ExcludeFromCodeCoverage]
public record VendorArticleValueDto
{
    public double TotalArticleValue { get; set; } = 0;
    public double TotalSalesValue { get; init; } = 0;
    public double TotalHandlingFee { get; init; } = 0;
    public double TotalSalesCommission { get; init; } = 0;
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