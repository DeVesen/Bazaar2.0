using System.Text;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;
using DeVesen.Bazaar.Shared.Basics;

namespace DeVesen.Bazaar.Server.Extensions;

public static class VendorExtensions
{
    public static VendorEntity ToEntity(this Vendor entity)
        => new()
        {
            Id = entity.Id,
            Salutation = entity.Salutation,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Address = entity.Address,
            EMail = entity.EMail,
            Phone = entity.Phone,
            Note = entity.Note
        };

    public static Vendor ToDomain(this VendorEntity entity)
        => new()
        {
            Id = entity.Id,
            Salutation = entity.Salutation,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Address = entity.Address,
            EMail = entity.EMail,
            Phone = entity.Phone,
            Note = entity.Note
        };

    public static Vendor ToDomain(this (string id, VendorCreateDto dto) data)
        => new()
        {
            Id = data.id,
            Salutation = data.dto.Salutation,
            FirstName = data.dto.FirstName,
            LastName = data.dto.LastName,
            Address = data.dto.Address,
            EMail = data.dto.EMail,
            Phone = data.dto.Phone,
            Note = data.dto.Note
        };

    public static Vendor ToDomain(this (string id, VendorUpdateDto dto) data)
        => new()
        {
            Id = data.id,
            Salutation = data.dto.Salutation,
            FirstName = data.dto.FirstName,
            LastName = data.dto.LastName,
            Address = data.dto.Address,
            EMail = data.dto.EMail,
            Phone = data.dto.Phone,
            Note = data.dto.Note
        };

    public static VendorDto ToDto(this Vendor data)
        => new()
        {
            Id = data.Id,
            Salutation = data.Salutation,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Address = data.Address,
            EMail = data.EMail,
            Phone = data.Phone,
            Note = data.Note
        };

    public static string ToShortHash(this VendorCreateDto dto)
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