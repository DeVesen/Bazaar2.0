using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public class ArticleActions
{
    public record Fetch(string? DealerId, string? Number, string? SearchText);

    public record SetList(IEnumerable<Article> Items);

    public record FetchFailed;

    public record Clear;


    public record SetItem(Article Article);

    public record RemoveItem(Article Article);


    public record SetBadDealer(string DealerId);
}