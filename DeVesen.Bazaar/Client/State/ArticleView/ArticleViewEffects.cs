using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public class ArticleViewEffects(IState<ArticleViewState> articleViewState, ArticleService articleService)
{
    [EffectMethod]
    public async Task Fetch(ArticleViewActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await articleService.GetAllAsync(action.VendorId, action.Number, action.SearchText);

        if (response.IsValid is false)
        {
            dispatcher.Dispatch(new ArticleViewActions.FetchFailed());
        }

        var domainElements = response.Value
            .OrderBy(p => p.Number)
            .ThenBy(p => p.Description);

        dispatcher.Dispatch(new ArticleViewActions.SetList(domainElements));
    }

    [EffectMethod]
    public async Task LoadItem(ArticleViewActions.LoadItem action, IDispatcher dispatcher)
    {
        if (articleViewState.Value.IsLoaded is false)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(articleViewState.Value.ActualVendorId) is false &&
            articleViewState.Value.ActualVendorId != action.VendorId)
        {
            return;
        }

        var response = await articleService.GetByNumber(action.ArticleNumber);

        if (response.IsValid is false)
        {
            return;
        }

        dispatcher.Dispatch(new ArticleViewActions.SetItem(response.Value!));
    }
}