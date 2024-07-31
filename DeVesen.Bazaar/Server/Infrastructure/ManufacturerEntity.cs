namespace DeVesen.Bazaar.Server.Infrastructure;

public record ManufacturerEntity
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}