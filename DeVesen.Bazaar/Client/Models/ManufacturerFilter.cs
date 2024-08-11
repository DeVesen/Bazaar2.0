using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Client.Models;

[ExcludeFromCodeCoverage]
public record ManufacturerFilter
{
    public string? Name { get; init; } = null;
    public string? SearchText { get; init; } = null;
}