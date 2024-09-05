namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryActions
{
    public record Fetch(string? Name, string? SearchText);

    public record Set(IEnumerable<Models.ArticleCategory> Items);

    public record FetchFailed();
}