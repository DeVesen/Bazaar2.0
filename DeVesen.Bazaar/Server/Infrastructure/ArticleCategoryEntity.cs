namespace DeVesen.Bazaar.Server.Infrastructure;

public record ArticleCategoryEntity
{
    public required string Id { get; init; }
    public required string Name { get; init; }
}