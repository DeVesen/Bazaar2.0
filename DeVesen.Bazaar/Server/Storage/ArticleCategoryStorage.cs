using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Hubs;
using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Storage
{
    public class ArticleCategoryStorage(IArticleCategoryRepository articleCategoryRepository, ArticleCategoryHubContext articleCategoryHubContext)
    {
        public async Task<bool> ExistByIdAsync(string id)
            => await articleCategoryRepository.ExistByIdAsync(id);

        public async Task<bool> ExistByNameAsync(string name)
            => await articleCategoryRepository.ExistByNameAsync(name);


        public Task<ArticleCategoryEntity> GetByNameAsync(string name)
            => articleCategoryRepository.GetByNameAsync(name);

        public async Task<IEnumerable<ArticleCategoryEntity>> GetAllAsync()
            => await articleCategoryRepository.GetAllAsync();


        public async Task CreateAsync(ArticleCategoryEntity entity)
        {
            await articleCategoryRepository.CreateAsync(entity);

            await articleCategoryHubContext.SendAdded(entity);
        }

        public async Task UpdateAsync(ArticleCategoryEntity entity)
        {
            await articleCategoryRepository.UpdateAsync(entity);

            await articleCategoryHubContext.SendUpdated(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await articleCategoryRepository.DeleteAsync(id);

            await articleCategoryHubContext.SendRemoved(id);
        }
    }
}
