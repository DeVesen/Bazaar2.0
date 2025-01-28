using Fluxor;

namespace SmartBazaar.Client.States.ArticleBrandState;

public class ArticleBrandFacade(IDispatcher dispatcher)
{
    public void FetchAll()
        => dispatcher.Dispatch(new ArticleBrandActions.FetchAll());
}