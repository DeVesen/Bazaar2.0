using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Contracts;

public interface IArticleCategoryRepository
{
    public Task<bool> ExistAsync(Guid id);

    public Task<bool> ExistAsync(string name);

    public Task<ArticleCategoryEntity> GetAsync(Guid id);

    public Task<ArticleCategoryEntity> GetAsync(string name);

    public Task<IEnumerable<ArticleCategoryEntity>> GetAllAsync();

    public Task CreateAsync(ArticleCategoryEntity entity);

    public Task UpdateAsync(ArticleCategoryEntity entity);

    public Task DeleteAsync(Guid id);
}