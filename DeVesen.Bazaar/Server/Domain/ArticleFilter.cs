﻿namespace DeVesen.Bazaar.Server.Domain;

public record ArticleFilter
{
    public string? VendorId { get; init; } = null;
    public string? Number { get; init; } = null;
    public string? SearchText { get; init; } = null;
}