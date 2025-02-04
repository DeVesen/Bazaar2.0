using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleCategoryState;

public class ArticleCategoryActions
{
    public record FetchAll;

    public record SetAll(ArticleCategoryModel[] Elements);

    public record FetchAllFailed;


    public record UpdateElement(ArticleCategoryModel Element);

    public record RemoveElement(string Id);
}