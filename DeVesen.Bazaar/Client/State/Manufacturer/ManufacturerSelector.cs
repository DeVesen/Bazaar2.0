using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public static class ManufacturerSelector
{
    public static bool IsLoading(this IState<ManufacturerState> state)
        => state.Value.IsLoading;

    public static IEnumerable<Models.Manufacturer> GetAllElements(this IState<ManufacturerState> state, string? filterValue = null)
    {
        return filterValue != null
            ? state.Value.Elements.Where(x => x.Name.Contains(filterValue, StringComparison.OrdinalIgnoreCase))
            : state.Value.Elements;
    }
}