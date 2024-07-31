using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Contracts;

public interface IArticleCategoryRepository
{
    public Task<bool> ExistByIdAsync(string id);

    public Task<bool> ExistByNameAsync(string name, string? allowedId);

    public Task<ArticleCategoryEntity> GetByIdAsync(string id);

    public Task<bool> ExistByNameAsync(string name);

    public Task<ArticleCategoryEntity> GetByNameAsync(string name);

    public Task<IEnumerable<ArticleCategoryEntity>> GetAllAsync();

    public Task CreateAsync(ArticleCategoryEntity entity);

    public Task UpdateAsync(ArticleCategoryEntity entity);

    public Task DeleteAsync(string id);
}