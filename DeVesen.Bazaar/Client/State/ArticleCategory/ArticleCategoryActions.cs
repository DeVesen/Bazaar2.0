namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryActions
{
    public record Fetch;

    public record Set(IEnumerable<Models.ArticleCategory> Elements);

    public record FetchFailed;


    public record AppendElement(Models.ArticleCategory Element);

    public record UpdateElement(Models.ArticleCategory Element);

    public record RemoveElement(string Id);
}