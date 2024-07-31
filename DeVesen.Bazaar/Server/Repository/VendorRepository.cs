using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Shared.Basics;
using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Server.Repository;

[ExcludeFromCodeCoverage]
public class VendorRepository : IVendorRepository
{
    private readonly List<VendorEntity> _innerList = new()
    {
        new()
        {
            Id = "DECB2A16-12DB-426C-8032-E7163530B16E".ToShortHash(),
            Salutation = "Male",
            FirstName = "Carsten",
            LastName = "Meyer",
            Address = "Street; ZIP Town",
            EMail = "Foo.Bar@FooBar.com",
            Phone = "123/123456789",
            Note = "Hat graue Haare"
        },
        new()
        {
            Id = "F2F9E5D2-BD17-41D5-AFA8-B829B83F5439".ToShortHash(),
            Salutation = "Male",
            FirstName = "Udo",
            LastName = "Scholz",
            Address = "Street; ZIP Town",
            EMail = "x.y@xy.com",
            Phone = "123/123456789",
            Note = "Hat braune Haare"
        },
        new()
        {
            Id = "95EB0C08-6D5F-4042-A0AC-301CF4BEDAFD".ToShortHash(),
            Salutation = "Female",
            FirstName = "Frauke",
            LastName = "Schmidt",
            Address = "Street; ZIP Town",
            EMail = "uuu.aaa@uuuaaa.com",
            Phone = "123/123456789",
            Note = "Hat blaue Augen"
        }
    };

    public async Task<bool> ExistByIdAsync(string id)
    {
        await Task.Delay(1);

        return _innerList.Any(p => p.Id == id);
    }

    public async Task<VendorEntity> GetAsync(string id)
    {
        await Task.Delay(1);

        return _innerList.First(p => p.Id == id);
    }

    public async Task<IEnumerable<VendorEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return _innerList.AsEnumerable();
    }

    public async Task CreateAsync(VendorEntity entity)
    {
        _innerList.Add(entity);

        await Task.Delay(100);
    }

    public async Task UpdateAsync(VendorEntity entity)
    {
        var elementIndex = _innerList.FindIndex(p => p.Id == entity.Id);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException(ResourceText.Transform(ResourceText.Vendor.NotFoundById, _ => entity.Id));
        }

        _innerList[elementIndex] = entity;

        await Task.Delay(100);
    }

    public async Task DeleteAsync(string id)
    {
        var element = _innerList.FirstOrDefault(p => p.Id == id);

        if (element == null)
        {
            throw new InvalidOperationException(ResourceText.Transform(ResourceText.Vendor.NotFoundById, _ => id));
        }

        _innerList.Remove(element);

        await Task.Delay(100);
    }
}