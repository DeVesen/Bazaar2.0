using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;

namespace DeVesen.Bazaar.Client.Extensions;

public static class VendorCategoryCategoryExtensions
{
    public static VendorCreateDto ToCreateDto(this Vendor data)
    {
        return new VendorCreateDto
        {
            Salutation = data.Salutation,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Address = data.Address,
            EMail = data.EMail,
            Phone = data.Phone,
            Note = data.Note
        };
    }

    public static VendorUpdateDto ToUpdateDto(this Vendor data)
    {
        return new VendorUpdateDto
        {
            Salutation = data.Salutation,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Address = data.Address,
            EMail = data.EMail,
            Phone = data.Phone,
            Note = data.Note
        };
    }
}