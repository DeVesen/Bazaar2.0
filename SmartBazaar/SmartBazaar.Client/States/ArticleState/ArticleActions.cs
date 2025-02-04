using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleState;

public class ArticleActions
{
    public record FetchAll;

    public record SetAll(ArticleModel[] Elements);

    public record FetchAllFailed;


    public record UpdateElement(ArticleModel Element);

    public record RemoveElement(string Id);

    public record RemoveDealer(string Id);
}