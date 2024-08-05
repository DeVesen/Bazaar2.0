using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Client.Models;

[ExcludeFromCodeCoverage]
public record ArticleFilter
{
    public string? VendorId { get; init; } = null;
    public string? Number { get; init; } = null;
    public string? SearchText { get; init; } = null;
}