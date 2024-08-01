using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record VendorDto
{
    public required string Id { get; init; }
    public required string Salutation { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Address { get; init; }
    public string? EMail { get; init; }
    public string? Phone { get; init; }
    public string? Note { get; init; }
}

[ExcludeFromCodeCoverage]
public record VendorCreateDto
{
    public required string Salutation { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Address { get; init; }
    public string? EMail { get; init; }
    public string? Phone { get; init; }
    public string? Note { get; init; }
}

[ExcludeFromCodeCoverage]
public record VendorUpdateDto
{
    public required string Salutation { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Address { get; init; }
    public string? EMail { get; init; }
    public string? Phone { get; init; }
    public string? Note { get; init; }
}

[ExcludeFromCodeCoverage]
public record VendorCreatedDto
{
    public required string Id { get; init; }
}