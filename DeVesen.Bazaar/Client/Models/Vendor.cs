using DeVesen.Bazaar.Shared.Extensions;

namespace DeVesen.Bazaar.Client.Models;

public record Vendor
{
    public string Id { get; init; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? EMail { get; set; }
    public string? Phone { get; set; }
    public string? Note { get; set; }

    public double OfferUnitPrice { get; set; } = 0.50d;
    public double SalesShare { get; set; } = 0.15d;

    public static Vendor New => new();

    public bool Contains(string text)
    {
        return text.BiContainsIgnoreCase(Id) ||
               text.BiContainsIgnoreCase(FirstName) ||
               text.BiContainsIgnoreCase(LastName) ||
               text.BiContainsIgnoreCase(Note);
    }

    public string GetTotalName()
    {
        return $"{LastName}, {FirstName}";
    }
}