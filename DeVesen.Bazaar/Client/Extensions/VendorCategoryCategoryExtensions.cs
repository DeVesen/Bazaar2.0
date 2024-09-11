using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared;

namespace DeVesen.Bazaar.Client.Extensions;

public static class VendorCategoryCategoryExtensions
{
    public static VendorCreateDto ToCreateDto(this Vendor data)
    {
        return new VendorCreateDto
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            Address = data.Address,
            EMail = data.EMail,
            Phone = data.Phone,
            Note = data.Note,
            OfferUnitPrice = data.OfferUnitPrice,
            SalesShare = data.SalesShare
        };
    }

    public static VendorUpdateDto ToUpdateDto(this Vendor data)
    {
        return new VendorUpdateDto
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            Address = data.Address,
            EMail = data.EMail,
            Phone = data.Phone,
            Note = data.Note,
            OfferUnitPrice = data.OfferUnitPrice,
            SalesShare = data.SalesShare
        };
    }
}