using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public class ManufacturerFacade
{
    private readonly ManufacturerState _manufacturerState;
    private readonly IDispatcher _dispatcher;

    public ManufacturerFacade(IState<ManufacturerState> manufacturerState, IDispatcher dispatcher)
    {
        _manufacturerState = manufacturerState.Value;
        _dispatcher = dispatcher;
    }

    public IEnumerable<Models.Manufacturer> GetManufacturer() => _manufacturerState.Vendors;

    public void Fetch() => Fetch(new ManufacturerFilter());

    public void Fetch(ManufacturerFilter filter) => _dispatcher.Dispatch(new ManufacturerActions.FetchManufacturers(filter));
}