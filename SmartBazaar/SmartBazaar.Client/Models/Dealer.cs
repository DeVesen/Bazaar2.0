using SmartBazaar.Shared.Extensions;

namespace SmartBazaar.Client.Models;

public class Dealer
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public string EMail { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;

    public double SalesShare { get; set; } = 0.15d;
    public double OfferUnitPrice { get; set; } = 0.50d;

    public static Dealer New => new()
    {
        Id = null!,
        FirstName = string.Empty,
        LastName = string.Empty,
        Address = string.Empty
    };

    public bool Contains(string value)
    {
        return value.BiContainsIgnoreCase(Id) ||
               value.BiContainsIgnoreCase(FirstName) ||
               value.BiContainsIgnoreCase(LastName) ||
               value.BiContainsIgnoreCase(Note);
    }

    public string GetTotalName()
    {
        return $"{LastName}, {FirstName}";
    }
}