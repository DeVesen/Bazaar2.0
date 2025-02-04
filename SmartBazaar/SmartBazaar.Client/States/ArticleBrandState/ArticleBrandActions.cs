using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleBrandState;

public class ArticleBrandActions
{
    public record FetchAll;

    public record SetAll(ArticleBrandModel[] Elements);

    public record FetchAllFailed;


    public record UpdateElement(ArticleBrandModel Element);

    public record RemoveElement(string Id);
}