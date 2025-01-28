using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public class ManufacturerEffects(ManufacturerService manufacturerService)
{
    [EffectMethod]
    public async Task Fetch(ManufacturerActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await manufacturerService.GetAllAsync(action.Name, action.SearchText);

        if (response.IsSuccess is false)
        {
            dispatcher.Dispatch(new ManufacturerActions.FetchFailed());
        }

        var domainElements = response.Elements
            .OrderBy(p => p.Name)
            .ToArray();

        dispatcher.Dispatch(new ManufacturerActions.Set(domainElements));
    }
}