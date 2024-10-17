using System.Text;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Basics;

namespace DeVesen.Bazaar.Server.Extensions;

public static class VendorExtensions
{

    public static VendorEntity ToDomain(this VendorCreateDto dto)
        => new()
        {
            Id = dto.ToShortHash(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            EMail = dto.EMail,
            Phone = dto.Phone,
            Note = dto.Note,
            OfferUnitPrice = dto.OfferUnitPrice,
            SalesShare = dto.SalesShare
        };

    public static VendorEntity ToDomain(this (string id, VendorUpdateDto dto) data)
        => new()
        {
            Id = data.id,
            FirstName = data.dto.FirstName,
            LastName = data.dto.LastName,
            Address = data.dto.Address,
            EMail = data.dto.EMail,
            Phone = data.dto.Phone,
            Note = data.dto.Note,
            OfferUnitPrice = data.dto.OfferUnitPrice,
            SalesShare = data.dto.SalesShare
        };

    public static VendorDto ToDto(this VendorEntity data)
        => new()
        {
            Id = data.Id,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Address = data.Address,
            EMail = data.EMail,
            Phone = data.Phone,
            Note = data.Note,
            OfferUnitPrice = data.OfferUnitPrice,
            SalesShare = data.SalesShare
        };

    private static string ToShortHash(this VendorCreateDto dto)
    {
        var builder = new StringBuilder();

        builder.Append(dto.FirstName);
        builder.Append(dto.LastName);
        builder.Append(dto.Address);
        builder.Append(dto.Phone);
        builder.Append(Guid.NewGuid().ToString());
        builder.Append(DateTime.UtcNow.Ticks);

        return builder.ToString().ToShortHash();
    }
}