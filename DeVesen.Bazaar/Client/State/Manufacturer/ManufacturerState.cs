using DeVesen.Bazaar.Client.Models;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

[FeatureState]
public record ManufacturerState(IEnumerable<Models.Manufacturer> Vendors, ManufacturerFilter FilterData, bool IsLoaded)
{
    private ManufacturerState() : this(Enumerable.Empty<Models.Manufacturer>(), new ManufacturerFilter(), false) { }
}