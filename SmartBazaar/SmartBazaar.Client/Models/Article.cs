namespace SmartBazaar.Client.Models;

public record Article
{
    public string Id { get; init; } = string.Empty;
    public string VendorId { get; init; } = string.Empty;


    // Description
    public required ArticleDescription Description { get; set; }

    // Price
    public required ArticlePrice Price { get; set; }

    // Status
    public required ArticleStatus Status { get; set; }


    public static Article CreateEmpty()
        => new()
        {
            Description = ArticleDescription.CreateEmpty(),
            Price = ArticlePrice.CreateEmpty(),
            Status = ArticleStatus.CreateEmpty()
        };
}


public record ArticleDescription
{
    public required string Number { get; set; }
    public required string ArticleBrand { get; set; }
    public required string ArticleCategory { get; set; }
    public required string Description { get; set; }


    public static ArticleDescription CreateEmpty()
        => new()
        {
            Number = string.Empty,
            ArticleBrand = string.Empty,
            ArticleCategory = string.Empty,
            Description = string.Empty,
        };
}

public record ArticlePrice
{
    public required double Price01 { get; set; }
    public double? Price02 { get; set; } = null;


    public static ArticlePrice CreateEmpty()
        => new()
        {
            Price01 = 0
        };
}

public record ArticleStatus
{
    public required DateTime Created { get; init; }
    public DateTime? ApprovedForSale { get; set; } = null;
    public DateTime? Sold { get; set; } = null;
    public double? SoldAt { get; set; } = null;
    public DateTime? Returned { get; set; } = null;
    public DateTime? Settled { get; set; } = null;


    public static ArticleStatus CreateEmpty()
        => new()
        {
            Created = DateTime.Now
        };
}


public static class ArticleExtensions
{
    public static bool IsApprovedForSale(this ArticleStatus articleStatus) => articleStatus.ApprovedForSale.HasValue;

    public static bool IsSold(this ArticleStatus articleStatus) => articleStatus is { ApprovedForSale: not null, Sold: not null } &&
                                                                   articleStatus.Returned.HasValue is false;

    public static bool IsReturned(this ArticleStatus articleStatus) => articleStatus is { ApprovedForSale: not null, Returned: not null } &&
                                                                       articleStatus.Sold.HasValue is false;

    public static bool IsSettled(this ArticleStatus articleStatus) => articleStatus is { ApprovedForSale: not null, Settled: not null };

    public static bool IsInSale(this ArticleStatus articleStatus) => articleStatus.ApprovedForSale.HasValue &&
                                                                     articleStatus.Sold.HasValue is false &&
                                                                     articleStatus.Returned.HasValue is false &&
                                                                     articleStatus.Settled.HasValue is false;

    public static string GetStatusText(this ArticleStatus articleStatus)
    {
        if (articleStatus.IsApprovedForSale())
        {
            return "---";
        }

        if (articleStatus.IsSold())
        {
            return "Verkauft";
        }
        if (articleStatus.IsReturned())
        {
            return "Zurückgegeben";
        }
        if (articleStatus.IsSettled())
        {
            return "Abgerechnet";
        }

        return "Im Verkauf";
    }
}