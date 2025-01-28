namespace SmartBazaar.Client.Models;

public record ArticleBrand
{
    public required string Id { get; init; }

    public required string Title { get; set; }

    public string? ImgSrc { get; set; }

    public static ArticleBrand CreateEmptyNew()
        => new()
        {
            Id = null!,
            Title = string.Empty,
            ImgSrc = string.Empty
        };
}