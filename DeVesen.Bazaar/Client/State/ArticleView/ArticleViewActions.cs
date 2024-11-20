using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public class ArticleViewActions
{
    public record Fetch(string? VendorId, string? Number, string? SearchText);

    public record SetList(IEnumerable<Article> Items);

    public record FetchFailed;

    public record Clear;


    public record SetItem(Article Article);

    public record RemoveItem(Article Article);


    public record SetBadVendor(string VendorId);
}