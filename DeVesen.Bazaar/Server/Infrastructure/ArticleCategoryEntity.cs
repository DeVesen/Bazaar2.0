namespace DeVesen.Bazaar.Server.Infrastructure;

public record ArticleCategoryEntity
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}