using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Contracts;

public interface IManufacturerRepository
{
    public Task<bool> ExistByIdAsync(string id);

    public Task<bool> ExistByNameAsync(string name);

    public Task<bool> ExistByNameAsync(string name, string? id);

    public Task<ManufacturerEntity> GetByIdAsync(string id);

    public Task<ManufacturerEntity> GetByNameAsync(string name);

    public Task<IEnumerable<ManufacturerEntity>> GetAllAsync();

    public Task CreateAsync(ManufacturerEntity entity);

    public Task UpdateAsync(ManufacturerEntity entity);

    public Task DeleteAsync(string id);
}