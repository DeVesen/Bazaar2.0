using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleState;

public class ArticleFacade(IDispatcher dispatcher)
{
    public void FetchAll()
        => dispatcher.Dispatch(new ArticleActions.FetchAll());

    public void UpdateElement(ArticleModel element)
        => dispatcher.Dispatch(new ArticleActions.UpdateElement(element));

    public void RemoveElement(string id)
        => dispatcher.Dispatch(new ArticleActions.RemoveElement(id));
}