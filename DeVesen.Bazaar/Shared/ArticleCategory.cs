using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record ArticleCategoryDto(Guid Id, string Name);

[ExcludeFromCodeCoverage]
public record ArticleCategoryCreateDto(string Name);

[ExcludeFromCodeCoverage]
public record ArticleCategoryUpdateDto(string Name);