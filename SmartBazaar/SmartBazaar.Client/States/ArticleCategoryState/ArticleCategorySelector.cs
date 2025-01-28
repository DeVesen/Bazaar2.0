using Fluxor;
using SmartBazaar.Client.Models;

namespace SmartBazaar.Client.States.ArticleCategoryState;

public static class ArticleCategorySelector
{
    public static bool IsLoading(this IState<ArticleCategoryState> state)
        => state.Value.IsLoading;

    public static void WaitLoadingIsDone(this IState<ArticleCategoryState> state)
    {
        Supporter.WaitUntil(() => state.IsLoading() is false)
                 .Wait();
    }


    public static IEnumerable<ArticleCategory> GetAll(this IState<ArticleCategoryState> state, string? filterValue = null)
    {
        state.WaitLoadingIsDone();

        return filterValue != null
            ? state.Value.Elements.Where(x => x.Title.Contains(filterValue, StringComparison.OrdinalIgnoreCase))
            : state.Value.Elements;
    }


    public static ArticleCategory? GetByIdAsync(this IState<ArticleCategoryState> state, string id)
    {
        state.WaitLoadingIsDone();

        return state.Value.Elements.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }

    public static ArticleCategory? GetByTitleAsync(this IState<ArticleCategoryState> state, string title)
    {
        state.WaitLoadingIsDone();

        return state.Value.Elements.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }


    public static bool TryGetByIdAsync(this IState<ArticleCategoryState> state, string id, out ArticleCategory element)
    {
        var foundElement = state.GetByIdAsync(id);

        element = foundElement ?? default!;

        return foundElement != null;
    }

    public static bool TryGetByTitleAsync(this IState<ArticleCategoryState> state, string title, out ArticleCategory element)
    {
        var foundElement = state.GetByTitleAsync(title);

        element = foundElement ?? default!;

        return foundElement != null;
    }
}