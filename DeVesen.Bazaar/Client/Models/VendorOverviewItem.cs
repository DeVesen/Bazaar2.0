namespace DeVesen.Bazaar.Client.Models;

public record VendorOverviewItem
{
    public required Vendor Vendor { get; set; }
    public required VendorArticleStock ArticleStock { get; set; }
}

public record VendorSettlementItem
{
    public required Vendor Vendor { get; set; }
    public required VendorArticleStock ArticleStock { get; set; }
    public required VendorArticleValue ArticleValue { get; init; }
    public required IEnumerable<Article> Articles { get; set; }
}