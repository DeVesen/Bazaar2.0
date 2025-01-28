using SmartBazaar.Client.Models;

namespace SmartBazaar.Client.States.ArticleCategoryState;

public class ArticleCategoryActions
{
    public record FetchAll;

    public record SetAll(ArticleCategory[] Elements);

    public record FetchAllFailed;


    public record UpdateElement(ArticleCategory Element);

    public record RemoveElement(string Id);
}