using DeVesen.Bazaar.Client.Models;

namespace DeVesen.Bazaar.Client.State.Manufacturer;

public class ManufacturerActions
{
    public record FetchManufacturers(ManufacturerFilter Filter);

    public record ManufacturersFetched(IEnumerable<Models.Manufacturer> Vendors);
}