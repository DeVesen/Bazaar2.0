namespace DeVesen.Bazaar.Server.Domain;

public record Manufacturer
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}