using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record ManufacturerDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}

[ExcludeFromCodeCoverage]
public record ManufacturerCreateDto
{
    public required string Name { get; init; }
}

[ExcludeFromCodeCoverage]
public record ManufacturerUpdateDto
{
    public required string Name { get; init; }
}