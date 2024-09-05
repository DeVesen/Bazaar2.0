using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public class ManufacturerEffects(ManufacturerService manufacturerService)
{
    [EffectMethod]
    public async Task FetchVendors(ManufacturerActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await manufacturerService.GetAllAsync(action.Name, action.SearchText);

        if (response.IsValid is false)
        {
            dispatcher.Dispatch(new ManufacturerActions.FetchFailed());
        }

        var domainElements = response.Value
            .OrderBy(p => p.Name)
            .Select(dtoElement => new Models.Manufacturer
            {
                Id = dtoElement.Id,
                Name = dtoElement.Name
            })
            .ToArray();

        dispatcher.Dispatch(new ManufacturerActions.Set(domainElements));
    }
}