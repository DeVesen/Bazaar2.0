namespace DeVesen.Bazaar.Client.State.Manufacturer;

public class ManufacturerActions
{
    public record Fetch(string? Name, string? SearchText);

    public record Set(IEnumerable<Models.Manufacturer> Items);

    public record FetchFailed;


    public record AppendElement(Models.Manufacturer Element);

    public record UpdateElement(Models.Manufacturer Element);

    public record RemoveElement(string Id);
}