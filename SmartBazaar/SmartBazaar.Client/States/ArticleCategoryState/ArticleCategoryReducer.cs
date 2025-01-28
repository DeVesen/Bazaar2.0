using Fluxor;

namespace SmartBazaar.Client.States.ArticleCategoryState;

public static class ArticleCategoryReducer
{
    [ReducerMethod(typeof(ArticleCategoryActions.FetchAll))]
    public static ArticleCategoryState FetchAll(ArticleCategoryState state)
        => state with { Elements = [], IsLoading = true };

    [ReducerMethod]
    public static ArticleCategoryState SetAll(ArticleCategoryState state, ArticleCategoryActions.SetAll action)
        => state with { Elements = action.Elements, IsLoading = false };

    [ReducerMethod(typeof(ArticleCategoryActions.FetchAllFailed))]
    public static ArticleCategoryState FetchAllFailed(ArticleCategoryState state)
        => state with { IsLoading = false };


    [ReducerMethod]
    public static ArticleCategoryState UpdateElement(ArticleCategoryState state, ArticleCategoryActions.UpdateElement action)
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
    public static ArticleCategoryState RemoveElement(ArticleCategoryState state, ArticleCategoryActions.RemoveElement action)
    {
        var elements = state.Elements.ToList();

        elements.RemoveAll(p => p.Id == action.Id);

        return state with { Elements = elements };
    }
}