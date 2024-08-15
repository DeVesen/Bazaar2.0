using Fluxor;

namespace DeVesen.Bazaar.Client.State.SalesCart;

public class SalesCartFacade
{
    public IDispatcher Dispatcher { get; }

    public SalesCartFacade(IDispatcher dispatcher)
    {
        Dispatcher = dispatcher;
    }

    public void ClearCart()
        => Dispatcher.Dispatch(new SalesCartActions.ClearCart());

    public void RequestItemToCart(Models.Article article, double salesAmount)
    {
        Dispatcher.Dispatch(new SalesCartActions.RequestItemToCart(article.Number, salesAmount, article));
    }

    public void RemoveItemFromCart(long articleNumber)
        => Dispatcher.Dispatch(new SalesCartActions.RemoveItemFromCart(articleNumber));

    public void CompleteSale()
        => Dispatcher.Dispatch(new SalesCartActions.CompleteSale());
}