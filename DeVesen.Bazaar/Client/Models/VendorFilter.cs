using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Client.Models;

[ExcludeFromCodeCoverage]
public record VendorFilter
{
    public string? Id { get; init; } = null;
    public string? Salutation { get; init; } = null;
    public string? SearchText { get; init; } = null;
}