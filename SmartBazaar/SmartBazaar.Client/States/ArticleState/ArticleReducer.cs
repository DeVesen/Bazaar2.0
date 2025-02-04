using Fluxor;

namespace SmartBazaar.Client.States.ArticleState;

public static class ArticleReducer
{
    [ReducerMethod(typeof(ArticleActions.FetchAll))]
    public static ArticleState FetchAll(ArticleState state)
        => state with { Elements = [], IsLoading = true };

    [ReducerMethod]
    public static ArticleState SetAll(ArticleState state, ArticleActions.SetAll action)
        => state with { Elements = action.Elements, IsLoading = false };

    [ReducerMethod(typeof(ArticleActions.FetchAllFailed))]
    public static ArticleState FetchAllFailed(ArticleState state)
        => state with { IsLoading = false };


    [ReducerMethod]
    public static ArticleState UpdateElement(ArticleState state, ArticleActions.UpdateElement action)
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
    public static ArticleState RemoveElement(ArticleState state, ArticleActions.RemoveElement action)
    {
        var elements = state.Elements.ToList();

        elements.RemoveAll(p => p.Id == action.Id);

        return state with { Elements = elements };
    }

    [ReducerMethod]
    public static ArticleState RemoveElement(ArticleState state, ArticleActions.RemoveDealer action)
    {
        var elements = state.Elements.ToList();

        elements.RemoveAll(p => p.DealerId == action.Id);

        return state with { Elements = elements };
    }
}