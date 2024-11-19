using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public static class ArticleViewReducer
{
    [ReducerMethod]
    public static ArticleViewState Fetch(ArticleViewState state, ArticleViewActions.Fetch action)
        => state with { Items = [], IsLoaded = false, ActualVendorId = action.VendorId, BadVendor = false };

    [ReducerMethod]
    public static ArticleViewState Set(ArticleViewState state, ArticleViewActions.SetList action)
        => state with { Items = action.Items, IsLoaded = true };

    [ReducerMethod]
    public static ArticleViewState Clear(ArticleViewState state, ArticleViewActions.Clear action)
        => state with { Items = [], IsLoaded = true };

    [ReducerMethod(typeof(ArticleViewActions.FetchFailed))]
    public static ArticleViewState FetchFailed(ArticleViewState state)
        => state with { IsLoaded = true };


    [ReducerMethod]
    public static ArticleViewState SetItem(ArticleViewState state, ArticleViewActions.SetItem action)
    {
        if (string.IsNullOrWhiteSpace(state.ActualVendorId) is false && state.ActualVendorId != action.Article.VendorId)
        {
            return state;
        }

        var newStateList = state.Items.ToList();
        var existingItem = newStateList.FirstOrDefault(p => p.Id == action.Article.Id);

        if (existingItem == null)
        {
            newStateList.Add(action.Article);
        }
        else
        {
            var existingItemIndex = newStateList.IndexOf(existingItem);
            newStateList[existingItemIndex] = action.Article;
        }

        return state with { Items = newStateList };
    }


    [ReducerMethod]
    public static ArticleViewState RemoveItem(ArticleViewState state, ArticleViewActions.RemoveItem action)
    {
        var newList = state.Items.Where(p => p.Id != action.ArticleId)
                                 .ToArray();

        return state with { Items = newList };
    }


    [ReducerMethod]
    public static ArticleViewState SetBadVendor(ArticleViewState state, ArticleViewActions.SetBadVendor action)
    {
        var newList = state.Items.Where(p => p.VendorId != action.VendorId)
            .ToArray();

        var actualIsBadVendor = state.ActualVendorId == action.VendorId;

        return state with { Items = newList, BadVendor = actualIsBadVendor };
    }
}