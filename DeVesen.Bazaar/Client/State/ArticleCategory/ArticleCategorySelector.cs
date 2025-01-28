using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public static class ArticleCategorySelector
{
    public static bool IsLoading(this IState<ArticleCategoryState> state)
        => state.Value.IsLoading;

    public static IEnumerable<Models.ArticleCategory> GetAllElements(this IState<ArticleCategoryState> state, string? filterValue = null)
    {
        return filterValue != null
            ? state.Value.Elements.Where(x => x.Name.Contains(filterValue, StringComparison.OrdinalIgnoreCase))
            : state.Value.Elements;
    }
}