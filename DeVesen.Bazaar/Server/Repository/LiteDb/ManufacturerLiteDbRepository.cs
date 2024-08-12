using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;
using LiteDB;

namespace DeVesen.Bazaar.Server.Repository.LiteDb;

[ExcludeFromCodeCoverage]
public class ManufacturerLiteDbRepository : IManufacturerRepository
{
    private readonly ILiteCollection<ManufacturerEntity> _dbCollection;

    public ManufacturerLiteDbRepository(ILiteDbEngine dbEngine)
    {
        _dbCollection = dbEngine.GetCollection<ManufacturerEntity>(nameof(ManufacturerEntity));
    }

    private IEnumerable<ManufacturerEntity> GetAllEntities() => _dbCollection.FindAll().ToArray();

    public async Task<bool> ExistByIdAsync(string id)
    {
        var element = GetAllEntities().FirstOrDefault(x => x.Id == id);
        return await Task.FromResult(element != null);
    }

    public async Task<bool> ExistByNameAsync(string name)
        => await ExistByNameAsync(name, null);

    public async Task<bool> ExistByNameAsync(string name, string? id)
    {
        var element = GetAllEntities().FirstOrDefault(x => x.Name == name && x.Id != id);
        return await Task.FromResult(element != null);
    }

    public async Task<ManufacturerEntity> GetByIdAsync(string id)
    {
        var element = GetAllEntities().FirstOrDefault(x => x.Id == id);
        return await Task.FromResult(element!);
    }

    public async Task<ManufacturerEntity> GetByNameAsync(string name)
    {
        var element = GetAllEntities().FirstOrDefault(x => x.Name == name);
        return await Task.FromResult(element!);
    }

    public async Task<IEnumerable<ManufacturerEntity>> GetAllAsync()
    {
        var element = _dbCollection.FindAll();
        return await Task.FromResult(element);
    }

    public async Task CreateAsync(ManufacturerEntity entity)
    {
        _dbCollection.Insert(entity.Id, entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(ManufacturerEntity entity)
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