using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record ManufacturerDto(Guid Id, string Name);

[ExcludeFromCodeCoverage]
public record ManufacturerCreateDto(string Name);

[ExcludeFromCodeCoverage]
public record ManufacturerUpdateDto(string Name);