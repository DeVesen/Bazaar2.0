namespace DeVesen.Bazaar.Server.Domain;

public record VendorArticleStatistic
{
    public required string VendorId { get; init; }
    public long NotOpen { get; set; } = 0;
    public long Open { get; set; } = 0;
    public long Sold { get; set; } = 0;
    public long Settled { get; set; } = 0;
    public double Turnover { get; set; } = 0;
}