using SmartBazaar.Client.Models;

namespace SmartBazaar.Client.States.ArticleBrandState;

public class ArticleBrandActions
{
    public record FetchAll;

    public record SetAll(ArticleBrand[] Elements);

    public record FetchAllFailed;
}