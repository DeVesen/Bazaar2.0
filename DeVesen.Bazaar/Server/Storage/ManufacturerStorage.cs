using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Hubs;
using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Storage;

public class ManufacturerStorage(IManufacturerRepository manufacturerRepository, ManufacturerHubContext manufacturerHubContext)
{
    public async Task<bool> ExistByIdAsync(string id)
        => await manufacturerRepository.ExistByIdAsync(id);

    public async Task<bool> ExistByNameAsync(string name)
        => await manufacturerRepository.ExistByNameAsync(name);


    public async Task<ManufacturerEntity> GetByNameAsync(string name)
        => await manufacturerRepository.GetByNameAsync(name);

    public async Task<IEnumerable<ManufacturerEntity>> GetAllAsync()
        => await manufacturerRepository.GetAllAsync();


    public async Task CreateAsync(ManufacturerEntity entity)
    {
        await manufacturerRepository.CreateAsync(entity);

        await manufacturerHubContext.SendAdded(entity);
    }

    public async Task UpdateAsync(ManufacturerEntity entity)
    {
        await manufacturerRepository.UpdateAsync(entity);

        await manufacturerHubContext.SendUpdated(entity);
    }

    public async Task DeleteAsync(string id)
    {
        if (await manufacturerRepository.ExistByIdAsync(id) is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        await manufacturerRepository.DeleteAsync(id);

        await manufacturerHubContext.SendRemoved(id);
    }
}