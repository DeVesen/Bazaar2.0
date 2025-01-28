using DeVesen.Bazaar.Client.Extensions;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryFacade(IDispatcher dispatcher, ArticleCategoryHubConnectionService articleCategoryHub)
{
    public void Fetch()
        => dispatcher.Dispatch(new ArticleCategoryActions.Fetch());


    public async Task StartCallbacks()
    {
        if (articleCategoryHub.IsWorking())
        {
            return;
        }

        await articleCategoryHub.StartAsync();

        articleCategoryHub.RegisterOnAdded(OnAdded);
        articleCategoryHub.RegisterOnUpdated(OnUpdated);
        articleCategoryHub.RegisterOnRemoved(OnRemoved);
    }


    public async Task StopCallbacks()
    {
        await articleCategoryHub.StopAsync();
    }


    private void OnAdded(Models.ArticleCategory element)
    {
        dispatcher.Dispatch(new ArticleCategoryActions.AppendElement(element));
    }

    private void OnUpdated(Models.ArticleCategory element)
    {
        dispatcher.Dispatch(new ArticleCategoryActions.UpdateElement(element));
    }

    private void OnRemoved(string id)
    {
        dispatcher.Dispatch(new ArticleCategoryActions.RemoveElement(id));
    }
}