namespace DeVesen.Bazaar.Server.Domain;

public record ArticleFilter
{
    public string? VendorId { get; init; } = null;
    public long? Number { get; init; } = null;
    public string? Title { get; init; } = null;
    public string? ArticleCategory { get; init; } = null;
    public string? Manufacturer { get; init; } = null;
}