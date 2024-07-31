namespace DeVesen.Bazaar.Client.Models;

public record ArticleCategory
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }

    public static ArticleCategory New => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty
    };
}