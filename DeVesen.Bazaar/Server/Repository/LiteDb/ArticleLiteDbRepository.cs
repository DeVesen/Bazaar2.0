using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;
using LiteDB;

namespace DeVesen.Bazaar.Server.Repository.LiteDb;

[ExcludeFromCodeCoverage]
public class ArticleLiteDbRepository : IArticleRepository
{
    private readonly ILiteCollection<ArticleEntity> _dbCollection;

    public ArticleLiteDbRepository(ILiteDbEngine dbEngine)
    {
        _dbCollection = dbEngine.GetCollection<ArticleEntity>(nameof(ArticleEntity));
    }

    public async Task<bool> ExistByIdAsync(string id)
    {
        var element = _dbCollection.FindOne(x => x.Id == id);
        return await Task.FromResult(element != null);
    }

    public async Task<bool> ExistByNumberAsync(long number)
        => await ExistByNumberAsync(number, null);

    public async Task<bool> ExistByNumberAsync(long number, string? allowedId)
    {
        var element = _dbCollection.FindOne(x => x.Number == number && x.Id != allowedId);
        return await Task.FromResult(element != null);
    }

    public async Task<(bool Exist, ArticleEntity? Entity)> TryGetByIdAsync(string id)
    {
        var element = _dbCollection.FindOne(x => x.Id == id);
        return await Task.FromResult((element != null, element));
    }

    public async Task<(bool Exist, ArticleEntity? Entity)> TryGetByNumberAsync(long number)
    {
        var element = _dbCollection.FindOne(x => x.Number == number);
        return await Task.FromResult((element != null, element));
    }

    public async Task<IEnumerable<ArticleEntity>> GetAllAsync()
    {
        var element = _dbCollection.FindAll();
        return await Task.FromResult(element);
    }

    public async Task CreateAsync(ArticleEntity entity)
    {
        _dbCollection.Insert(entity.Id, entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(ArticleEntity entity)
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