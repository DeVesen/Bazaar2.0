namespace DeVesen.Bazaar.Client.Models;

public record VendorView
{
    public required Vendor Item { get; set; }
    public required VendorArticleStatistic Statistic { get; set; }
}