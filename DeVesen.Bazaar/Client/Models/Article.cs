﻿using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Client.Models;

[ExcludeFromCodeCoverage]
public record Article
{
    public string Id { get; init; } = string.Empty;
    public required string VendorId { get; init; }
    public long Number { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ArticleCategory { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public DateTime Created { get; init; }
    public double Price01 { get; set; }
    public double? Price02 { get; set; }
    public string? Description { get; set; }
    public DateTime? ApprovedForSale { get; set; }
    public DateTime? Sold { get; set; }
    public double? SoldAt { get; set; }
    public DateTime? Settled { get; set; }

    public static Article CreateNew(string vendorId) => new()
    {
        VendorId = vendorId
    };

    public bool Contains(string text)
    {
        text = text.Trim().ToLower();

        return VendorId.ToLower().Contains(text) ||
               Title.ToLower().Contains(text) ||
               ArticleCategory.ToLower().Contains(text) ||
               Manufacturer.ToLower().Contains(text);
    }
}