using System.Text;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Basics;

namespace DeVesen.Bazaar.Server.Extensions;

public static class ArticleExtensions
{
    public static ArticleEntity ToEntity(this Article data)
        => new()
        {
            Id = data.Id,
            VendorId = data.VendorId,
            Number = data.Number,
            Title = data.Title,
            ArticleCategory = data.ArticleCategory,
            Manufacturer = data.Manufacturer,
            Created = data.Created,
            ApprovedForSale = data.ApprovedForSale,
            Sold = data.Sold,
            SoldAt = data.SoldAt,
            Settled = data.Settled,
            Price01 = data.Price01,
            Price02 = data.Price02,
            Description = data.Description
        };

    public static Article ToDomain(this ArticleEntity entity)
        => new()
        {
            Id = entity.Id,
            VendorId = entity.VendorId,
            Number = entity.Number,
            Title = entity.Title,
            ArticleCategory = entity.ArticleCategory,
            Manufacturer = entity.Manufacturer,
            Created = entity.Created,
            ApprovedForSale = entity.ApprovedForSale,
            Sold = entity.Sold,
            SoldAt = entity.SoldAt,
            Settled = entity.Settled,
            Price01 = entity.Price01,
            Price02 = entity.Price02,
            Description = entity.Description
        };

    public static Article ToDomain(this ArticleCreateDto dto)
        => new()
        {
            Id = dto.ToShortHash(),
            VendorId = dto.VendorId,
            Number = dto.Number,
            Title = dto.Title,
            ArticleCategory = dto.ArticleCategory,
            Manufacturer = dto.Manufacturer,
            Created = dto.ApprovedForSale ?? DateTime.Now,
            ApprovedForSale = dto.ApprovedForSale,
            Sold = null,
            SoldAt = null,
            Settled = null,
            Price01 = dto.Price01,
            Price02 = dto.Price02,
            Description = dto.Description
        };

    public static Article ToDomain(this (string id, ArticleUpdateDto dto) data)
        => new()
        {
            Id = data.id,
            VendorId = data.dto.VendorId,
            Number = data.dto.Number,
            Title = data.dto.Title,
            ArticleCategory = data.dto.ArticleCategory,
            Manufacturer = data.dto.Manufacturer,
            Created = data.dto.Created,
            ApprovedForSale = data.dto.ApprovedForSale,
            Sold = data.dto.Sold,
            SoldAt = data.dto.SoldAt,
            Settled = data.dto.Settled,
            Price01 = data.dto.Price01,
            Price02 = data.dto.Price02,
            Description = data.dto.Description
        };

    public static ArticleDto ToDto(this Article data)
        => new()
        {
            Id = data.Id,
            VendorId = data.VendorId,
            Number = data.Number,
            Title = data.Title,
            ArticleCategory = data.ArticleCategory,
            Manufacturer = data.Manufacturer,
            Created = data.Created,
            ApprovedForSale = data.ApprovedForSale,
            Sold = data.Sold,
            SoldAt = data.SoldAt,
            Settled = data.Settled,
            Price01 = data.Price01,
            Price02 = data.Price02,
            Description = data.Description
        };

    public static ArticleFilter ToDomain(this ArticleFilterDto dto)
        => new()
        {
            VendorId = dto.VendorId,
            Number = dto.Number,
            Title = dto.Title,
            ArticleCategory = dto.ArticleCategory,
            Manufacturer = dto.Manufacturer
        };

    private static string ToShortHash(this ArticleCreateDto dto)
    {
        var builder = new StringBuilder();

        builder.Append(dto.VendorId);
        builder.Append(dto.Number);
        builder.Append(dto.Title);
        builder.Append(dto.ArticleCategory);
        builder.Append(dto.Manufacturer);
        builder.Append(Guid.NewGuid().ToString());
        builder.Append(DateTime.UtcNow.Ticks);

        return builder.ToString().ToShortHash();
    }
}