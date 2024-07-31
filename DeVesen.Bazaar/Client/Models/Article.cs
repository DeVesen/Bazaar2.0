namespace DeVesen.Bazaar.Client.Models;

public record Article
{
    public string Id { get; init; } = string.Empty;
    public string VendorId { get; init; } = string.Empty;
    public long Number { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ArticleCategory { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public DateTime Created { get; init; }
    public double Price01 { get; set; }
    public double? Price02 { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime? ApprovedForSale { get; set; }
    public DateTime? Sold { get; set; }
    public double? SoldAt { get; set; }
    public DateTime? Settled { get; set; }

    public static Article New => new();

    public bool Contains(string text)
    {
        text = text.Trim().ToLower();

        return VendorId.ToLower().Contains(text) ||
               Title.ToLower().Contains(text) ||
               ArticleCategory.ToLower().Contains(text) ||
               Manufacturer.ToLower().Contains(text);
    }
}