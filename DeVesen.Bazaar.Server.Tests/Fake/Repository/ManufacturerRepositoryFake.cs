using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Tests.Fake.Repository;

[ExcludeFromCodeCoverage]
public class ManufacturerRepositoryFake : IManufacturerRepository
{
    public readonly List<ManufacturerEntity> InnerList;

    public ManufacturerRepositoryFake(params ManufacturerEntity[] elements)
    {
        InnerList = elements.ToList();
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        await Task.Delay(1);

        return InnerList.Any(p => p.Id == id);
    }

    public async Task<bool> ExistAsync(string name, Guid? allowedId = null)
    {
        await Task.Delay(1);

        return InnerList.Any(p => p.Name == name && p.Id != allowedId);
    }

    public async Task<ManufacturerEntity> GetAsync(Guid id)
    {
        await Task.Delay(1);

        return InnerList.First(p => p.Id == id);
    }

    public async Task<ManufacturerEntity> GetAsync(string name)
    {
        await Task.Delay(1);

        return InnerList.First(p => p.Name == name);
    }

    public async Task<IEnumerable<ManufacturerEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return InnerList.AsEnumerable();
    }

    public async Task CreateAsync(ManufacturerEntity entity)
    {
        InnerList.Add(entity);

        await Task.Delay(100);
    }

    public async Task UpdateAsync(ManufacturerEntity entity)
    {
        var elementIndex = InnerList.FindIndex(p => p.Id == entity.Id);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException($"Manufacturer '{entity.Id}' nicht gefunden!");
        }

        InnerList[elementIndex] = entity;

        await Task.Delay(100);
    }

    public async Task DeleteAsync(Guid id)
    {
        var element = InnerList.FirstOrDefault(p => p.Id == id);

        if (element == null)
        {
            throw new InvalidOperationException($"Manufacturer '{id}' nicht gefunden!");
        }

        InnerList.Remove(element);

        await Task.Delay(100);
    }
}