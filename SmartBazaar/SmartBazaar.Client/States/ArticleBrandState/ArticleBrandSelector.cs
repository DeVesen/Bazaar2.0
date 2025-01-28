using Fluxor;
using SmartBazaar.Client.Models;

namespace SmartBazaar.Client.States.ArticleBrandState;

public static class ArticleBrandSelector
{
    public static bool IsLoading(this IState<ArticleBrandState> state)
        => state.Value.IsLoading;

    public static void WaitLoadingIsDone(this IState<ArticleBrandState> state)
    {
        Supporter.WaitUntil(() => state.IsLoading() is false)
                 .Wait();
    }


    public static IEnumerable<ArticleBrand> GetAll(this IState<ArticleBrandState> state, string? filterValue = null)
    {
        state.WaitLoadingIsDone();

        return filterValue != null
            ? state.Value.Elements.Where(x => x.Title.Contains(filterValue, StringComparison.OrdinalIgnoreCase))
            : state.Value.Elements;
    }


    public static ArticleBrand? GetByIdAsync(this IState<ArticleBrandState> state, string id)
    {
        state.WaitLoadingIsDone();

        return state.Value.Elements.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }

    public static ArticleBrand? GetByTitleAsync(this IState<ArticleBrandState> state, string title)
    {
        state.WaitLoadingIsDone();

        return state.Value.Elements.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }


    public static bool TryGetByIdAsync(this IState<ArticleBrandState> state, string id, out ArticleBrand element)
    {
        var foundElement = state.GetByIdAsync(id);

        element = foundElement ?? default!;

        return foundElement != null;
    }

    public static bool TryGetByTitleAsync(this IState<ArticleBrandState> state, string title, out ArticleBrand element)
    {
        var foundElement = state.GetByTitleAsync(title);

        element = foundElement ?? default!;

        return foundElement != null;
    }
}