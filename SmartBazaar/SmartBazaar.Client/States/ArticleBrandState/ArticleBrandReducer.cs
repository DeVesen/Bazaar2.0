using Fluxor;

namespace SmartBazaar.Client.States.ArticleBrandState;

public static class ArticleBrandReducer
{
    [ReducerMethod(typeof(ArticleBrandActions.FetchAll))]
    public static ArticleBrandState FetchAll(ArticleBrandState state)
        => state with { Elements = [], IsLoading = true };

    [ReducerMethod]
    public static ArticleBrandState SetAll(ArticleBrandState state, ArticleBrandActions.SetAll action)
        => state with { Elements = action.Elements, IsLoading = false };

    [ReducerMethod(typeof(ArticleBrandActions.FetchAllFailed))]
    public static ArticleBrandState FetchAllFailed(ArticleBrandState state)
        => state with { IsLoading = false };


    [ReducerMethod]
    public static ArticleBrandState UpdateElement(ArticleBrandState state, ArticleBrandActions.UpdateElement action)
    {
        var elements = state.Elements.ToList();

        var elementIndex = elements.FindIndex(0, p => p.Id == action.Element.Id);

        if (elementIndex < 0)
        {
            elements.Add(action.Element);
        }
        else
        {
            elements[elementIndex] = action.Element;
        }

        return state with { Elements = elements };
    }
    
    [ReducerMethod]
    public static ArticleBrandState RemoveElement(ArticleBrandState state, ArticleBrandActions.RemoveElement action)
    {
        var elements = state.Elements.ToList();

        elements.RemoveAll(p => p.Id == action.Id);

        return state with { Elements = elements };
    }
}