using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public class ManufacturerFacade(IDispatcher dispatcher)
{
    public void Fetch(string? name = null, string? searchText = null)
        => dispatcher.Dispatch(new ManufacturerActions.Fetch(name, searchText));
}