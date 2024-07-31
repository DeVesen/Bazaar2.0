using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;
using System.Text;
using DeVesen.Bazaar.Shared.Basics;

namespace DeVesen.Bazaar.Server.Extensions;

public static class ManufacturerExtensions
{
    public static ManufacturerEntity ToEntity(this Manufacturer entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    public static Manufacturer ToDomain(this ManufacturerEntity entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    public static Manufacturer ToDomain(this ManufacturerCreateDto dto)
        => new()
        {
            Id = dto.ToShortHash(),
            Name = dto.Name
        };

    public static Manufacturer ToDomain(this (string id, ManufacturerUpdateDto dto) data)
        => new()
        {
            Id = data.id,
            Name = data.dto.Name
        };

    public static ManufacturerDto ToDto(this Manufacturer data)
        => new(data.Id, data.Name);

    private static string ToShortHash(this ManufacturerCreateDto dto)
    {
        var builder = new StringBuilder();

        builder.Append(dto.Name);
        builder.Append(Guid.NewGuid().ToString());
        builder.Append(DateTime.UtcNow.Ticks);

        return builder.ToString().ToShortHash();
    }
}