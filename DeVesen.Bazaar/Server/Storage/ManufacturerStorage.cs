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

    public async Task<bool> ExistByIdAsync(string id)
    {
        return await _manufacturerRepository.ExistByIdAsync(id);
    }

    public async Task<bool> ExistByNameAsync(string name)
    {
        return await _manufacturerRepository.ExistByNameAsync(name);
    }

    public async Task<bool> ExistByNameAsync(string name, string? id)
    {
        return await _manufacturerRepository.ExistByNameAsync(name, id);
    }

    public async Task<Manufacturer> GetByIdAsync(string id)
    {
        if (await _manufacturerRepository.ExistByIdAsync(id) is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        var element = await _manufacturerRepository.GetByIdAsync(id);
        return element.ToDomain();
    }

    public async Task<Manufacturer> GetByNameAsync(string name)
    {
        if (await _manufacturerRepository.ExistByNameAsync(name) is false)
        {
            throw new InvalidDataException($"SearchText '{name}' not found!");
        }

        var element = await _manufacturerRepository.GetByNameAsync(name);
        return element.ToDomain();
    }

    public async Task<IEnumerable<Manufacturer>> GetAllAsync()
    {
        var elements = await _manufacturerRepository.GetAllAsync();
        return elements.Select(p => p.ToDomain());
    }

    public async Task CreateAsync(Manufacturer element)
    {
        if (await _manufacturerRepository.ExistByIdAsync(element.Id))
        {
            throw new InvalidDataException($"Id '{element.Id}' already exist!");
        }
        if (await _manufacturerRepository.ExistByNameAsync(element.Name))
        {
            throw new InvalidDataException($"SearchText '{element.Name}' already exist!");
        }

        await _manufacturerRepository.CreateAsync(element.ToEntity());
    }

    public async Task UpdateAsync(Manufacturer element)
    {
        if (await _manufacturerRepository.ExistByIdAsync(element.Id) is false)
        {
            throw new InvalidDataException($"Id '{element.Id}' not found!");
        }
        if (await _manufacturerRepository.ExistByNameAsync(element.Name))
        {
            var entity = await _manufacturerRepository.GetByNameAsync(element.Name);

            if (entity != null && entity.Id != element.Id)
            {
                throw new InvalidDataException($"SearchText '{element.Name}' already exist!");
            }
        }

        await _manufacturerRepository.UpdateAsync(element.ToEntity());
    }

    public async Task DeleteAsync(string id)
    {
        if (await _manufacturerRepository.ExistByIdAsync(id) is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        await _manufacturerRepository.DeleteAsync(id);
    }
}