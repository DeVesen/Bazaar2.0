using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleBrandState;

public class ArticleBrandFacade(IDispatcher dispatcher)
{
    public void FetchAll()
        => dispatcher.Dispatch(new ArticleBrandActions.FetchAll());

    public void UpdateElement(ArticleBrandModel element)
        => dispatcher.Dispatch(new ArticleBrandActions.UpdateElement(element));

    public void RemoveElement(string id)
        => dispatcher.Dispatch(new ArticleBrandActions.RemoveElement(id));
}