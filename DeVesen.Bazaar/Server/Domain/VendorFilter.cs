namespace DeVesen.Bazaar.Server.Domain;

public record VendorFilter
{
    public string? Id { get; init; } = null;
    public string? SearchText { get; init; } = null;
}