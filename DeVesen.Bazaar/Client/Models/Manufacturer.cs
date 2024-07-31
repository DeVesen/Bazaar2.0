namespace DeVesen.Bazaar.Client.Models;

public record Manufacturer
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }

    public static Manufacturer New => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty
    };
}