using Fluxor;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.ArticleBrandState;

public static class ArticleBrandSelector
{
    public static bool IsLoading(this IState<ArticleBrandState> state)
        => state.Value.IsLoading;


    public static void WaitLoadingIsDone(this IState<ArticleBrandState> state)
        => Supporter.WaitUntil(() => state.IsLoading() is false);


    public static IEnumerable<ArticleBrandModel> GetAll(this IState<ArticleBrandState> state, string? filterValue = null)
    {
        state.WaitLoadingIsDone();

        return filterValue != null
            ? state.Value.Elements.Where(x => x.Title.Contains(filterValue, StringComparison.OrdinalIgnoreCase))
            : state.Value.Elements;
    }


    public static ArticleBrandModel? GetByIdAsync(this IState<ArticleBrandState> state, string id)
    {
        state.WaitLoadingIsDone();

        return state.Value.Elements.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }

    public static ArticleBrandModel? GetByTitleAsync(this IState<ArticleBrandState> state, string title)
    {
        state.WaitLoadingIsDone();

        return state.Value.Elements.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }


    public static bool TryGetByIdAsync(this IState<ArticleBrandState> state, string id, out ArticleBrandModel element)
    {
        var foundElement = state.GetByIdAsync(id);

        element = foundElement ?? default!;

        return foundElement != null;
    }

    public static bool TryGetByTitleAsync(this IState<ArticleBrandState> state, string title, out ArticleBrandModel element)
    {
        var foundElement = state.GetByTitleAsync(title);

        element = foundElement ?? default!;

        return foundElement != null;
    }
}