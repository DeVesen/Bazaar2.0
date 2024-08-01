namespace DeVesen.Bazaar.Client.Models;

public record VendorView
{
    public required Vendor Item { get; set; }
    public required VendorArticleStatistic Statistic { get; set; }

    public bool Contains(string text)
    {
        return Item.Contains(text);
    }
}

public record VendorArticleStatistic
{
    public long Open { get; set; }
    public long Sold { get; set; }
    public long Settled { get; set; }
    public required double Turnover { get; init; }

    public override string ToString()
    {
        return $"{Open} / {Settled} / {Settled}";
    }
}