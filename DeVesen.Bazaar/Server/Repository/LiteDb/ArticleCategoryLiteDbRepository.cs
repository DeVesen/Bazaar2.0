using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;
using LiteDB;

namespace DeVesen.Bazaar.Server.Repository.LiteDb;

[ExcludeFromCodeCoverage]
public class ArticleCategoryLiteDbRepository(ILiteDbEngine dbEngine) : IArticleCategoryRepository
{
    private readonly ILiteCollection<ArticleCategoryEntity> _dbCollection = dbEngine.GetCollection<ArticleCategoryEntity>(nameof(ArticleCategoryEntity));

    private IEnumerable<ArticleCategoryEntity> GetAllEntities() => _dbCollection.FindAll().ToArray();


    public async Task<bool> ExistByIdAsync(string id)
    {
        var element = GetAllEntities().FirstOrDefault(x => x.Id == id);
        return await Task.FromResult(element != null);
    }

    public async Task<bool> ExistByNameAsync(string name)
    {
        var element = GetAllEntities().FirstOrDefault(x => x.Name == name);
        return await Task.FromResult(element != null);
    }


    public async Task<ArticleCategoryEntity> GetByNameAsync(string name)
    {
        var entity = GetAllEntities().FirstOrDefault(x => x.Name == name);
        return await Task.FromResult(entity!);
    }


    public async Task<IEnumerable<ArticleCategoryEntity>> GetAllAsync()
    {
        var element = _dbCollection.FindAll();
        return await Task.FromResult(element);
    }


    public async Task CreateAsync(ArticleCategoryEntity entity)
    {
        _dbCollection.Insert(entity.Id, entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(ArticleCategoryEntity entity)
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