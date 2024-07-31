using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;

namespace DeVesen.Bazaar.Server.Storage;

public class VendorStorage
{
    private readonly IVendorRepository _vendorRepository;

    public VendorStorage(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public async Task<bool> ExistByIdAsync(string id)
    {
        return await _vendorRepository.ExistByIdAsync(id);
    }

    public async Task<IEnumerable<Vendor>> GetAllAsync()
    {
        var elements = await _vendorRepository.GetAllAsync();
        return elements.Select(p => p.ToDomain());
    }

    public async Task CreateAsync(Vendor element)
    {
        if (await _vendorRepository.ExistByIdAsync(element.Id))
        {
            throw new InvalidDataException($"Id '{element.Id}' already exist!");
        }

        await _vendorRepository.CreateAsync(element.ToEntity());
    }

    public async Task UpdateAsync(Vendor element)
    {
        if (await _vendorRepository.ExistByIdAsync(element.Id) is false)
        {
            throw new InvalidDataException($"Id '{element.Id}' not found!");
        }

        await _vendorRepository.UpdateAsync(element.ToEntity());
    }

    public async Task DeleteAsync(string id)
    {
        if (await _vendorRepository.ExistByIdAsync(id) is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        await _vendorRepository.DeleteAsync(id);
    }
}