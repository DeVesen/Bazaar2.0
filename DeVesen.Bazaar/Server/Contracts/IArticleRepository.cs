using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Contracts;

public interface IArticleRepository
{
    public Task<bool> ExistAsync(string id);

    public Task<bool> ExistAsync(long number);

    public Task<ArticleEntity> GetAsync(string id);

    public Task<ArticleEntity> GetAsync(long number);

    public Task<IEnumerable<ArticleEntity>> GetAllAsync();

    public Task CreateAsync(ArticleEntity entity);

    public Task UpdateAsync(ArticleEntity entity);

    public Task DeleteAsync(string id);
}