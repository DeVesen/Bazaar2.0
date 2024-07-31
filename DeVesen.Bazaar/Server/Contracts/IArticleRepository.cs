using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Contracts;

public interface IArticleRepository
{
    public Task<bool> ExistByIdAsync(string id);

    public Task<bool> ExistByNumberAsync(long number);

    public Task<bool> ExistByNumberAsync(long number, string? allowedId);

    public Task<(bool Exist, ArticleEntity? Entity)> TryGetByIdAsync(string id);

    public Task<(bool Exist, ArticleEntity? Entity)> TryGetByNumberAsync(long number);

    public Task<IEnumerable<ArticleEntity>> GetAllAsync();

    public Task CreateAsync(ArticleEntity entity);

    public Task UpdateAsync(ArticleEntity entity);

    public Task DeleteAsync(string id);
}