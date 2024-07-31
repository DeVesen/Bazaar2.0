namespace DeVesen.Bazaar.Client.Models;

public record Manufacturer
{
    public string? Id { get; init; }
    public string Name { get; set; } = string.Empty;

    public static Manufacturer New => new();
}