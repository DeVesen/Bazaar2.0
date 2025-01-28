using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public static class ArticleCategoryReducer
{
    [ReducerMethod(typeof(ArticleCategoryActions.Fetch))]
    public static ArticleCategoryState FetchVendors(ArticleCategoryState state)
        => state with { Elements = [], IsLoading = true };

    [ReducerMethod]
    public static ArticleCategoryState VendorsFetched(ArticleCategoryState state, ArticleCategoryActions.Set action)
        => state with { Elements = action.Elements.OrderBy(p => p.Name).ToArray(), IsLoading = false };

    [ReducerMethod(typeof(ArticleCategoryActions.FetchFailed))]
    public static ArticleCategoryState VendorsFetched(ArticleCategoryState state)
        => state with { IsLoading = false };


    [ReducerMethod]
    public static ArticleCategoryState AppendElement(ArticleCategoryState state, ArticleCategoryActions.AppendElement action)
    {
        var elements = state.Elements.ToList();

        elements.Add(action.Element);

        return state with { Elements = elements.OrderBy(p => p.Name).ToArray() };
    }

    [ReducerMethod]
    public static ArticleCategoryState UpdateElement(ArticleCategoryState state, ArticleCategoryActions.UpdateElement action)
    {
        var elements = state.Elements.ToList();
        var existingElement = elements.FirstOrDefault(p => p.Id == action.Element.Id);
        var existingIndex = existingElement != null ? elements.IndexOf(existingElement) : -1;

        if (existingIndex < 0)
        {
            return state;
        }

        elements[existingIndex] = action.Element;

        return state with { Elements = elements.OrderBy(p => p.Name).ToArray() };
    }

    [ReducerMethod]
    public static ArticleCategoryState RemoveElement(ArticleCategoryState state, ArticleCategoryActions.RemoveElement action)
    {
        var elements = state.Elements.ToList();

        elements.RemoveAll(p => p.Id == action.Id);

        return state with { Elements = elements.OrderBy(p => p.Name).ToArray() };
    }
}