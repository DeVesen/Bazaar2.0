using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Contracts;

public interface IManufacturerRepository
{
    public Task<bool> ExistAsync(Guid id);

    public Task<bool> ExistAsync(string name, Guid? allowedId = null);

    public Task<ManufacturerEntity> GetAsync(Guid id);

    public Task<ManufacturerEntity> GetAsync(string name);

    public Task<IEnumerable<ManufacturerEntity>> GetAllAsync();

    public Task CreateAsync(ManufacturerEntity entity);

    public Task UpdateAsync(ManufacturerEntity entity);

    public Task DeleteAsync(Guid id);
}