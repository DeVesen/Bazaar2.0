using Fluxor;
using SmartBazaar.Client.Models;

namespace SmartBazaar.Client.States.ArticleBrandState;

public static class ArticleBrandSelector
{
    public static bool IsLoading(this IState<ArticleBrandState> state)
        => state.Value.IsLoading;

    public static IEnumerable<ArticleBrand> GetAll(this IState<ArticleBrandState> state, string? filterValue = null)
    {
        return filterValue != null
            ? state.Value.Elements.Where(x => x.Title.Contains(filterValue, StringComparison.OrdinalIgnoreCase))
            : state.Value.Elements;
    }
}