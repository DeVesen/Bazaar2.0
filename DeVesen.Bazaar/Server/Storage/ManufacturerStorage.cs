using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;

namespace DeVesen.Bazaar.Server.Storage;

public class ManufacturerStorage
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public ManufacturerStorage(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    public async Task<bool> ExistByIdAsync(Guid id)
    {
        return await _manufacturerRepository.ExistAsync(id);
    }

    public async Task<bool> ExistByNameAsync(string name)
    {
        return await _manufacturerRepository.ExistAsync(name);
    }

    public async Task<Manufacturer> GetByNameAsync(string name)
    {
        if (await ExistByNameAsync(name) is false)
        {
            throw new InvalidDataException($"Name '{name}' not found!");
        }

        var element = await _manufacturerRepository.GetAsync(name);
        return element.ToDomain();
    }

    public async Task<IEnumerable<Manufacturer>> GetAllAsync()
    {
        var elements = await _manufacturerRepository.GetAllAsync();
        return elements.Select(p => p.ToDomain());
    }

    public async Task CreateAsync(Manufacturer element)
    {
        if (await ExistByIdAsync(element.Id))
        {
            throw new InvalidDataException($"Id '{element.Id}' already exist!");
        }
        if (await ExistByNameAsync(element.Name))
        {
            throw new InvalidDataException($"Name '{element.Name}' already exist!");
        }

        await _manufacturerRepository.CreateAsync(element.ToEntity());
    }

    public async Task UpdateAsync(Manufacturer element)
    {
        if (await ExistByIdAsync(element.Id) is false)
        {
            throw new InvalidDataException($"Id '{element.Id}' not found!");
        }
        if (await ExistByNameAsync(element.Name))
        {
            var entity = await GetByNameAsync(element.Name);

            if (entity != null && entity.Id != element.Id)
            {
                throw new InvalidDataException($"Name '{element.Name}' already exist!");
            }
        }

        await _manufacturerRepository.UpdateAsync(element.ToEntity());
    }

    public async Task DeleteAsync(Guid id)
    {
        if (await ExistByIdAsync(id) is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        await _manufacturerRepository.DeleteAsync(id);
    }
}