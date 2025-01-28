using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public static class ManufacturerReducer
{
    [ReducerMethod(typeof(ManufacturerActions.Fetch))]
    public static ManufacturerState FetchVendors(ManufacturerState state)
        => state with { Elements = Enumerable.Empty<Models.Manufacturer>(), IsLoading = true };

    [ReducerMethod]
    public static ManufacturerState VendorsFetched(ManufacturerState state, ManufacturerActions.Set action)
        => state with { Elements = action.Items, IsLoading = false };

    [ReducerMethod(typeof(ManufacturerActions.FetchFailed))]
    public static ManufacturerState VendorsFetched(ManufacturerState state)
        => state with { IsLoading = false };


    [ReducerMethod]
    public static ManufacturerState AppendElement(ManufacturerState state, ManufacturerActions.AppendElement action)
    {
        var elements = state.Elements.ToList();

        elements.Add(action.Element);

        return state with { Elements = elements.OrderBy(p => p.Name).ToArray() };
    }

    [ReducerMethod]
    public static ManufacturerState UpdateElement(ManufacturerState state, ManufacturerActions.UpdateElement action)
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
    public static ManufacturerState RemoveElement(ManufacturerState state, ManufacturerActions.RemoveElement action)
    {
        var elements = state.Elements.ToList();

        elements.RemoveAll(p => p.Id == action.Id);

        return state with { Elements = elements.OrderBy(p => p.Name).ToArray() };
    }
}