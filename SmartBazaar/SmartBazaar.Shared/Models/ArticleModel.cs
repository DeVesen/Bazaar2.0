namespace SmartBazaar.Shared.Models;

public class ArticleModel : ArticleDataModel
{
    public required string Id { get; init; }
}

public class ArticleCreateModel : ArticleDataModel
{
}

public class ArticleDataModel
{
    public required string DealerId { get; init; }
    public string Number { get; set; } = string.Empty;
    public ArticleDescription Description { get; set; } = new();
    public ArticlePrice Price { get; set; } = new();
    public ArticleStatus Status { get; set; } = new();
}


public record ArticleDescription
{
    public string ArticleBrand { get; set; } = string.Empty;
    public string ArticleCategory { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ArticlePrice
{
    public double Price01 { get; set; } = 0;
    public double? Price02 { get; set; } = null;
}

public class ArticleStatus
{
    public DateTime Created { get; init; } = DateTime.Now;
    public DateTime? ApprovedForSale { get; set; } = null;
    public DateTime? Sold { get; set; } = null;
    public double? SoldAt { get; set; } = null;
    public DateTime? Returned { get; set; } = null;
    public DateTime? Settled { get; set; } = null;
}