using SmartBazaar.Shared.Types;
using static MudBlazor.Defaults;

namespace SmartBazaar.Shared.Models;

public class DealerModel : DealerDataModel
{
    public required string Id { get; init; }
}

public class DealerCreateModel : DealerDataModel
{
}

public class DealerDataModel
{
    public NameModel Name { get; set; } = new();
    public AddressModel Address { get; set; } = new();
    public ContactModel Contact { get; set; } = new();
    public SalesShareRatioModel SalesShareRatio { get; set; } = new();
    public string Note { get; set; } = string.Empty;
}

public class NameModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class AddressModel
{
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}

public class ContactModel
{
    public string EMail { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class SalesShareRatioModel
{
    public DealerTypes Type { get; set; } = DealerTypes.Private;
    public double SalesShare { get; set; } = 0.15d;
    public double OfferUnitPrice { get; set; } = 0.50d;
}