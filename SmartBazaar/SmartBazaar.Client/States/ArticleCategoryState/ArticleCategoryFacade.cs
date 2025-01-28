using Fluxor;
using SmartBazaar.Client.Models;

namespace SmartBazaar.Client.States.ArticleCategoryState;

public class ArticleCategoryFacade(IDispatcher dispatcher)
{
    public void FetchAll()
        => dispatcher.Dispatch(new ArticleCategoryActions.FetchAll());

    public void UpdateElement(ArticleCategory element)
        => dispatcher.Dispatch(new ArticleCategoryActions.UpdateElement(element));

    public void RemoveElement(string id)
        => dispatcher.Dispatch(new ArticleCategoryActions.RemoveElement(id));
}