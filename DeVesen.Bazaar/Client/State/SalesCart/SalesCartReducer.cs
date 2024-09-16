using Fluxor;

namespace DeVesen.Bazaar.Client.State.SalesCart;

public class SalesCartReducer
{
    [ReducerMethod(typeof(SalesCartActions.ClearCart))]
    public static SalesCartState ClearCart(SalesCartState state)
    {
        return state with { PurchaseItems = [], CriticalWarnings = [], BookingInProcess = false };
    }

    [ReducerMethod(typeof(SalesCartActions.BookSaleCompleted))]
    public static SalesCartState SaleCompleted(SalesCartState state)
    {
        return state with { PurchaseItems = Enumerable.Empty<PurchaseItem>(), BookingInProcess = false };
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

    [ReducerMethod(typeof(SalesCartActions.BookSale))]
    public static SalesCartState BookSale(SalesCartState state)
    {
        return state with { BookingInProcess = true };
    }

    [ReducerMethod]
    public static SalesCartState AddCriticalWarning(SalesCartState state, SalesCartActions.AddCriticalWarning action)
    {
        if (state.BookingInProcess || state.PurchaseItems.Any() is false)
        {
            return state;
        }

        var criticalWarnings = state.CriticalWarnings.ToList();

        if (criticalWarnings.Any(p => p.ArticleNumber == action.Warning.ArticleNumber))
        {
            return state;
        }

        criticalWarnings.Add(action.Warning);

        return state with { CriticalWarnings = criticalWarnings };
    }
}