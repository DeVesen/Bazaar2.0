using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;

namespace DeVesen.Bazaar.Client.Extensions;

public static class ArticleExtensions
{
    public static ArticleCreateDto ToCreateDto(this Article data)
    {
        return new ArticleCreateDto
        {
            VendorId = data.VendorId,
            Number = data.Number,
            Title = data.Title,
            ArticleCategory = data.ArticleCategory,
            Manufacturer = data.Manufacturer,
            ApprovedForSale = data.ApprovedForSale,
            Price01 = data.Price01,
            Price02 = data.Price02,
            Description = data.Description
        };
    }

    public static ArticleUpdateDto ToUpdateDto(this Article data)
    {
        return new ArticleUpdateDto
        {
            VendorId = data.VendorId,
            Number = data.Number,
            Title = data.Title,
            ArticleCategory = data.ArticleCategory,
            Manufacturer = data.Manufacturer,
            Price01 = data.Price01,
            Price02 = data.Price02,
            Description = data.Description,
            Created = data.Created,
            ApprovedForSale = data.ApprovedForSale,
            Sold = data.Sold,
            SoldAt = data.SoldAt,
            Settled = data.Settled,
            Returned = data.Returned
        };
    }
}