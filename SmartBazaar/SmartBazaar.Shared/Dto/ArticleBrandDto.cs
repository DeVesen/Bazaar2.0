namespace SmartBazaar.Shared.Dto;

public record ArticleBrandDto
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public required string Url { get; init; }
}