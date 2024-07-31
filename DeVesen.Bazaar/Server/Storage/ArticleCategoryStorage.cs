using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;

namespace DeVesen.Bazaar.Server.Storage
{
    public class ArticleCategoryStorage
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryStorage(IArticleCategoryRepository articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _articleCategoryRepository.ExistAsync(id);
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _articleCategoryRepository.ExistAsync(name);
        }

        public async Task<ArticleCategory> GetByNameAsync(string name)
        {
            if (await ExistByNameAsync(name) is false)
            {
                throw new InvalidDataException($"Name '{name}' not found!");
            }

            var element = await _articleCategoryRepository.GetAsync(name);
            return element.ToDomain();
        }

        public async Task<IEnumerable<ArticleCategory>> GetAllAsync()
        {
            var elements = await _articleCategoryRepository.GetAllAsync();
            return elements.Select(p => p.ToDomain());
        }

        public async Task CreateAsync(ArticleCategory element)
        {
            if (await ExistByIdAsync(element.Id))
            {
                throw new InvalidDataException($"Id '{element.Id}' already exist!");
            }
            if (await ExistByNameAsync(element.Name))
            {
                throw new InvalidDataException($"Name '{element.Name}' already exist!");
            }

            await _articleCategoryRepository.CreateAsync(element.ToEntity());
        }

        public async Task UpdateAsync(ArticleCategory element)
        {
            if (await ExistByIdAsync(element.Id) is false)
            {
                throw new InvalidDataException($"Id '{element.Id}' not found!");
            }
            if (await ExistByNameAsync(element.Name))
            {
                var entity = await GetByNameAsync(element.Name);

                if (entity != null && entity.Id != element.Id)
                {
                    throw new InvalidDataException($"Name '{element.Name}' already exist!");
                }
            }

            await _articleCategoryRepository.UpdateAsync(element.ToEntity());
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await ExistByIdAsync(id) is false)
            {
                throw new InvalidDataException($"Id '{id}' not found!");
            }

            await _articleCategoryRepository.DeleteAsync(id);
        }
    }
}
