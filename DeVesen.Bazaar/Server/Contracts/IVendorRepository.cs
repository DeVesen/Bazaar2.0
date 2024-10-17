using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Contracts;

public interface IVendorRepository
{
    public Task<bool> ExistByIdAsync(string id);

    public Task<VendorEntity> GetAsync(string id);

    public Task<IEnumerable<VendorEntity>> GetAllAsync();

    public Task CreateAsync(VendorEntity entity);

    public Task UpdateAsync(VendorEntity entity);

    public Task DeleteAsync(string id);
}