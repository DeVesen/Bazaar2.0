using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared;

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

    public static Manufacturer ToDomain(this (Guid id, ManufacturerCreateDto dto) data)
        => new()
        {
            Id = data.id,
            Name = data.dto.Name
        };

    public static Manufacturer ToDomain(this (Guid id, ManufacturerUpdateDto dto) data)
        => new()
        {
            Id = data.id,
            Name = data.dto.Name
        };

    public static ManufacturerDto ToDto(this Manufacturer data)
        => new(data.Id, data.Name);
}