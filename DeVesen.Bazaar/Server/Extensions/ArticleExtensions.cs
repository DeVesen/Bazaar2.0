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
            Description = data.Description,
            ArticleCategory = data.ArticleCategory,
            Manufacturer = data.Manufacturer,
            Created = data.Created,
            ApprovedForSale = data.ApprovedForSale,
            Sold = data.Sold,
            SoldAt = data.SoldAt,
            Settled = data.Settled,
            Price01 = data.Price01,
            Price02 = data.Price02,
            Returned = data.Returned,
        };

    public static Article ToDomain(this ArticleEntity entity)
        => new()
        {
            Id = entity.Id,
            VendorId = entity.VendorId,
            Number = entity.Number,
            Description = entity.Description,
            ArticleCategory = entity.ArticleCategory,
            Manufacturer = entity.Manufacturer,
            Created = entity.Created,
            ApprovedForSale = entity.ApprovedForSale,
            Sold = entity.Sold,
            SoldAt = entity.SoldAt,
            Settled = entity.Settled,
            Price01 = entity.Price01,
            Price02 = entity.Price02,
            Returned = entity.Returned,
        };

    public static Article ToDomain(this ArticleCreateDto dto)
        => new()
        {
            Id = dto.ToShortHash(),
            VendorId = dto.VendorId,
            Number = dto.Number,

            ArticleCategory = dto.ArticleCategory,
            Manufacturer = dto.Manufacturer,
            Description = dto.Description,

            Price01 = dto.Price01,
            Price02 = dto.Price02,
            Created = dto.ApprovedForSale ?? DateTime.Now,

            ApprovedForSale = dto.ApprovedForSale,
            Sold = null,
            SoldAt = null,
            Returned = null,
            Settled = null
        };

    public static Article ToDomain(this (string id, ArticleUpdateDto dto) data)
        => new()
        {
            Id = data.id,
            VendorId = data.dto.VendorId,
            Number = data.dto.Number,

            ArticleCategory = data.dto.ArticleCategory,
            Manufacturer = data.dto.Manufacturer,
            Description = data.dto.Description,

            Created = data.dto.Created,
            Price01 = data.dto.Price01,
            Price02 = data.dto.Price02,

            ApprovedForSale = data.dto.ApprovedForSale,
            Sold = data.dto.Sold,
            SoldAt = data.dto.SoldAt,
            Returned = data.dto.Returned,
            Settled = data.dto.Settled
        };

    public static ArticleDto ToDto(this Article data)
        => new()
        {
            Id = data.Id,
            VendorId = data.VendorId,
            Number = data.Number,

            ArticleCategory = data.ArticleCategory,
            Manufacturer = data.Manufacturer,
            Description = data.Description,

            Price01 = data.Price01,
            Price02 = data.Price02,
            Created = data.Created,

            ApprovedForSale = data.ApprovedForSale,
            Sold = data.Sold,
            SoldAt = data.SoldAt,
            Returned = data.Returned,
            Settled = data.Settled,
        };

    public static VendorArticleStatisticDto ToDto(this VendorArticleStatistic data)
        => new()
        {
            NotOpen = data.NotOpen,
            Open = data.Open,
            Sold = data.Sold,
            Settled = data.Settled,
            Turnover = data.Turnover
        };

    public static SalesOrder.Position ToDomain(this SalesOrderDto.Position dto)
        => new(dto.Number, dto.Price);

    private static string ToShortHash(this ArticleCreateDto dto)
    {
        var builder = new StringBuilder();

        builder.Append(dto.VendorId);
        builder.Append(dto.Number);
        builder.Append(dto.Description);
        builder.Append(dto.ArticleCategory);
        builder.Append(dto.Manufacturer);
        builder.Append(Guid.NewGuid().ToString());
        builder.Append(DateTime.UtcNow.Ticks);

        return builder.ToString().ToShortHash();
    }
}