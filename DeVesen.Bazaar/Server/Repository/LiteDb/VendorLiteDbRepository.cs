using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;
using LiteDB;

namespace DeVesen.Bazaar.Server.Repository.LiteDb;

[ExcludeFromCodeCoverage]
public class VendorLiteDbRepository : IVendorRepository
{
    private readonly ILiteCollection<VendorEntity> _dbCollection;

    public VendorLiteDbRepository(ILiteDbEngine dbEngine)
    {
        _dbCollection = dbEngine.GetCollection<VendorEntity>(nameof(VendorEntity));
    }

    public async Task<bool> ExistByIdAsync(string id)
    {
        var element = _dbCollection.FindOne(x => x.Id == id);
        return await Task.FromResult(element != null);
    }

    public async Task<IEnumerable<VendorEntity>> GetAllAsync()
    {
        var element = _dbCollection.FindAll();
        return await Task.FromResult(element);
    }

    public async Task CreateAsync(VendorEntity entity)
    {
        _dbCollection.Insert(entity.Id, entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(VendorEntity entity)
    {
        _dbCollection.Update(entity.Id, entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(string id)
    {
        _dbCollection.Delete(id);
        await Task.CompletedTask;
    }
}