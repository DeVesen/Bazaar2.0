using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Tests.Fake.Repository;

[ExcludeFromCodeCoverage]
public class VendorRepositoryFake : IVendorRepository
{
    public readonly List<VendorEntity> InnerList;

    public VendorRepositoryFake(params VendorEntity[] elements)
    {
        InnerList = elements.ToList();
    }

    public async Task<bool> ExistByIdAsync(string id)
    {
        await Task.Delay(1);

        return InnerList.Any(p => p.Id == id);
    }

    public async Task<VendorEntity> GetAsync(string id)
    {
        await Task.Delay(1);

        return InnerList.First(p => p.Id == id);
    }

    public async Task<IEnumerable<VendorEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return InnerList.AsEnumerable();
    }

    public async Task CreateAsync(VendorEntity entity)
    {
        InnerList.Add(entity);

        await Task.Delay(100);
    }

    public async Task UpdateAsync(VendorEntity entity)
    {
        var elementIndex = InnerList.FindIndex(p => p.Id == entity.Id);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException($"Vendor '{entity.Id}' nicht gefunden!");
        }

        InnerList[elementIndex] = entity;

        await Task.Delay(100);
    }

    public async Task DeleteAsync(string id)
    {
        var element = InnerList.FirstOrDefault(p => p.Id == id);

        if (element == null)
        {
            throw new InvalidOperationException($"Vendor '{id}' nicht gefunden!");
        }

        InnerList.Remove(element);

        await Task.Delay(100);
    }
}