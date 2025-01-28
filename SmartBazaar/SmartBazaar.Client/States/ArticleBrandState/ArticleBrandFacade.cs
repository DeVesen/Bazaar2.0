using Fluxor;
using SmartBazaar.Client.Models;
using SmartBazaar.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartBazaar.Client.States.ArticleBrandState;

public class ArticleBrandFacade(IDispatcher dispatcher)
{
    public void FetchAll()
        => dispatcher.Dispatch(new ArticleBrandActions.FetchAll());

    public void UpdateElement(ArticleBrand element)
        => dispatcher.Dispatch(new ArticleBrandActions.UpdateElement(element));

    public void RemoveElement(string id)
        => dispatcher.Dispatch(new ArticleBrandActions.RemoveElement(id));
}