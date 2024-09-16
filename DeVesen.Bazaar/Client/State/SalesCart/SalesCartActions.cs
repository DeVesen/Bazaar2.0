namespace DeVesen.Bazaar.Client.State.SalesCart;

public class SalesCartActions
{
    public record ClearCart;

    public record RequestItemToCart(long ArticleNumber, double SalesAmount, Models.Article Article);

    public record AddItemToCart(long ArticleNumber, double SalesAmount, Models.Article Article);

    public record AddItemToCartFailed(long ArticleNumber, double SalesAmount, string Message);

    public record RemoveItemFromCart(long ArticleNumber);

    public record BookSale;

    public record BookSaleCompleted;


    public record AddCriticalWarning(CriticalWarning Warning);
}