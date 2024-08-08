using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;

namespace DeVesen.Bazaar.Client.Extensions;

public static class ManufacturerCategoryCategoryExtensions
{
    public static ManufacturerCreateDto ToCreateDto(this Manufacturer data)
    {
        return new ManufacturerCreateDto
        {
            Name = data.Name
        };
    }

    public static ManufacturerUpdateDto ToUpdateDto(this Manufacturer data)
    {
        return new ManufacturerUpdateDto
        {
            Name = data.Name
        };
    }
}