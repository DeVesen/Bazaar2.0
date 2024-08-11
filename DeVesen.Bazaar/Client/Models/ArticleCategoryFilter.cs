using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Client.Models;

[ExcludeFromCodeCoverage]
public record ArticleCategoryFilter
{
    public string? Name { get; init; } = null;
}