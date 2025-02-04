using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleView;

public static class ArticleReducer
{
    [ReducerMethod]
    public static ArticleState Fetch(ArticleState state, ArticleActions.Fetch action)
        => state with { Items = [], IsLoaded = false, ActualVendorId = action.DealerId, BadVendor = false };

    [ReducerMethod]
    public static ArticleState Set(ArticleState state, ArticleActions.SetList action)
        => state with { Items = action.Items, IsLoaded = true };

    [ReducerMethod]
    public static ArticleState Clear(ArticleState state, ArticleActions.Clear action)
        => state with { Items = [], IsLoaded = true };

    [ReducerMethod(typeof(ArticleActions.FetchFailed))]
    public static ArticleState FetchFailed(ArticleState state)
        => state with { IsLoaded = true };


    [ReducerMethod]
    public static ArticleState SetItem(ArticleState state, ArticleActions.SetItem action)
    {
        if (string.IsNullOrWhiteSpace(state.ActualVendorId) is false && state.ActualVendorId != action.Article.DealerId)
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
    public static ArticleState RemoveItem(ArticleState state, ArticleActions.RemoveItem action)
    {
        var newList = state.Items.Where(p => p.Id != action.Article.Id)
                                 .ToArray();

        return state with { Items = newList };
    }


    [ReducerMethod]
    public static ArticleState SetBadVendor(ArticleState state, ArticleActions.SetBadDealer action)
    {
        var newList = state.Items.Where(p => p.DealerId != action.DealerId)
            .ToArray();

        var actualIsBadVendor = state.ActualVendorId == action.DealerId;

        return state with { Items = newList, BadVendor = actualIsBadVendor };
    }
}