using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.Article;

public class ArticleActions
{
    public record Fetch(string? VendorId, string? Number, string? SearchText);

    public record Set(IEnumerable<Models.Article> Items);

    public record FetchFailed();
}