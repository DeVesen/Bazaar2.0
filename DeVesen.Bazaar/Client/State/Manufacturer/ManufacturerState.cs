using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

[FeatureState]
public record ManufacturerState(IEnumerable<Models.Manufacturer> Items, bool IsLoaded)
{
    private ManufacturerState() : this([], false) { }
}