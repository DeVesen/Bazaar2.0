using Fluxor;

namespace DeVesen.Bazaar.Client.State.SalesCart;

public class SalesCartReducer
{
    [ReducerMethod(typeof(SalesCartActions.ClearCart))]
    public static SalesCartState ClearCart(SalesCartState state)
    {
        return state with { PurchaseItems = Enumerable.Empty<PurchaseItem>() };
    }

    [ReducerMethod(typeof(SalesCartActions.SaleCompleted))]
    public static SalesCartState SaleCompleted(SalesCartState state)
    {
        return state with { PurchaseItems = Enumerable.Empty<PurchaseItem>() };
    }

    [ReducerMethod]
    public static SalesCartState AddItemToCart(SalesCartState state, SalesCartActions.AddItemToCart action)
    {
        var tmpPurchaseItems = state.PurchaseItems.ToList();

        tmpPurchaseItems.Add(new PurchaseItem
        {
            ArticleNumber = action.ArticleNumber,
            SalesAmount = action.SalesAmount,
            Article = action.Article
        });

        return state with { PurchaseItems = tmpPurchaseItems };
    }

    [ReducerMethod]
    public static SalesCartState RemoveItemFromCart(SalesCartState state, SalesCartActions.RemoveItemFromCart action)
    {
        var tmpPurchaseItems = state.PurchaseItems.ToList();

        tmpPurchaseItems.RemoveAll(p => p.ArticleNumber == action.ArticleNumber);

        return state with { PurchaseItems = tmpPurchaseItems };
    }
}