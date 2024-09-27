using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Client.Models;

[ExcludeFromCodeCoverage]
public record Article
{
    public enum StatusType
    {
        None,
        Approved,
        Sold,
        Returned,
        Settled
    }

    public string Id { get; init; } = string.Empty;
    public required string VendorId { get; init; }
    public long Number { get; set; }
    public string ArticleCategory { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double Price01 { get; set; }
    public double? Price02 { get; set; }
    public DateTime Created { get; init; }

    public DateTime? ApprovedForSale { get; set; }
    public DateTime? Sold { get; set; }
    public double? SoldAt { get; set; }
    public DateTime? Returned { get; set; }
    public DateTime? Settled { get; set; }

    public static Article CreateNew(string vendorId, long? number = null) => new()
    {
        VendorId = vendorId,
        Number = number ?? default
    };

    public bool IsValidForSale() => IsApprovedForSale() &&
                                    IsSold() is false &&
                                    IsReturned() is false &&
                                    IsSettled() is false;

    public StatusType Status => GetStatus();
    public string StatusText => GetStatusText();

    public bool IsApprovedForSale() => ApprovedForSale.HasValue;
    public bool IsSold() => Sold.HasValue;
    public bool IsReturned() => Returned.HasValue;
    public bool IsSettled() => Settled.HasValue;

    private StatusType GetStatus()
    {
        if (IsSettled())
        {
            return StatusType.Settled;
        }

        if (IsReturned())
        {
            return StatusType.Returned;
        }

        if (IsSold())
        {
            return StatusType.Sold;
        }

        if (IsApprovedForSale())
        {
            return StatusType.Approved;
        }

        return StatusType.None;
    }

    private string GetStatusText()
        => Status switch
        {
            StatusType.None => "Angelegt",
            StatusType.Approved => "Freigegeben",
            StatusType.Sold => "Verkauft",
            StatusType.Returned => "Zurückgegeben",
            StatusType.Settled => "Abgerechnet",
            _ => "F/N"
        };
}