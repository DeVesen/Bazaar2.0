namespace DeVesen.Bazaar.Client.Models;

public record VendorArticleStatistic
{
    public long NotOpen { get; set; }
    public long Open { get; set; }
    public long Sold { get; set; }
    public long Settled { get; set; }
    public required double Turnover { get; init; }

    public override string ToString()
    {
        return NotOpen > 0
            ? $"{Open} ({NotOpen}) / {Sold} / {Settled}"
            : $"{Open} / {Sold} / {Settled}";
    }
}