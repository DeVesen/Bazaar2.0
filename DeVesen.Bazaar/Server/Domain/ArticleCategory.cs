namespace DeVesen.Bazaar.Server.Domain;

public record ArticleCategory
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}