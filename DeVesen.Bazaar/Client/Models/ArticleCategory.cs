namespace DeVesen.Bazaar.Client.Models;

public record ArticleCategory
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static ArticleCategory New => new();
}