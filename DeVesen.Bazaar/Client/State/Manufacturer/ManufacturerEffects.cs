using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public class ManufacturerEffects
{
    private readonly ManufacturerService _manufacturerService;

    public ManufacturerEffects(ManufacturerService manufacturerService)
    {
        _manufacturerService = manufacturerService;
    }

    [EffectMethod]
    public async Task FetchVendors(ManufacturerActions.FetchManufacturers action, IDispatcher dispatcher)
    {
        var elements = await _manufacturerService.GetAllAsync(action.Filter);

        elements = elements.OrderBy(p => p.Name)
                           .ToArray();

        dispatcher.Dispatch(new ManufacturerActions.ManufacturersFetched(elements));
    }
}