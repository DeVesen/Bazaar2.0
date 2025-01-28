using Fluxor;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

[FeatureState]
public record ManufacturerState(IEnumerable<Models.Manufacturer> Elements, bool IsLoading)
{
    private ManufacturerState() : this([], false) { }
}