﻿namespace DeVesen.Bazaar.Server.Domain;

public record Vendor
{
    public required string Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Address { get; init; }
    public string? EMail { get; init; }
    public string? Phone { get; init; }
    public string? Note { get; init; }
    public double OfferUnitPrice { get; set; }
    public double SalesShare { get; set; }
}