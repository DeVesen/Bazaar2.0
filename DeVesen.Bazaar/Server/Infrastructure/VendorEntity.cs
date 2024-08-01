namespace DeVesen.Bazaar.Server.Infrastructure;

public record VendorEntity
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