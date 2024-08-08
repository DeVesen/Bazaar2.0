using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record ArticleCategoryDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}

[ExcludeFromCodeCoverage]
public record ArticleCategoryCreateDto
{
    public required string Name { get; init; }
}

[ExcludeFromCodeCoverage]
public record ArticleCategoryUpdateDto
{
    public required string Name { get; init; }
}