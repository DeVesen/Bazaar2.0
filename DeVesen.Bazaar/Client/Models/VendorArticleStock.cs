namespace DeVesen.Bazaar.Client.Models;

public record VendorArticleStock
{
    public long Recorded { get; set; }
    public long OnSale { get; init; }
    public long Sold { get; init; }
    public long Returned { get; init; }
    public long Settled { get; init; }

    public override string ToString()
    {
        return Recorded > 0
            ? $"{OnSale} ({Recorded}) / {Sold} / {Returned} / {Settled}"
            : $"{OnSale} / {Sold} / {Returned} / {Settled}";
    }
}

public record VendorArticleValue
{
    public double TotalArticleValue { get; set; }
    public double TotalSalesValue { get; init; }
    public double TotalHandlingFee { get; init; }
    public double TotalSalesCommission { get; init; }
}