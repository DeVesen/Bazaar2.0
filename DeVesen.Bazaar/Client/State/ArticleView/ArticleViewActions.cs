using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public class ArticleViewActions
{
    public record Fetch(string? VendorId, string? Number, string? SearchText);

    public record SetList(IEnumerable<Article> Items);

    public record FetchFailed;

    public record Clear;


    public record LoadItem(string VendorId, string ArticleId, long ArticleNumber);

    public record SetItem(Article Article);


    public record RemoveItem(string VendorId, string ArticleId, long ArticleNumber);


    public record SetBadVendor(string VendorId);
}