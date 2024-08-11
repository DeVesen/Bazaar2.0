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

        public async Task<bool> ExistByIdAsync(string id)
        {
            return await _articleCategoryRepository.ExistByIdAsync(id);
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _articleCategoryRepository.ExistByNameAsync(name);
        }

        public async Task<bool> ExistByNameAsync(string name, string? allowedId)
        {
            return await _articleCategoryRepository.ExistByNameAsync(name, allowedId);
        }

        public async Task<ArticleCategory> GetByIdAsync(string id)
        {
            if (await ExistByIdAsync(id) is false)
            {
                throw new InvalidDataException($"Id '{id}' not found!");
            }

            var element = await _articleCategoryRepository.GetByIdAsync(id);
            return element.ToDomain();
        }

        public async Task<ArticleCategory> GetByNameAsync(string name)
        {
            if (await ExistByNameAsync(name) is false)
            {
                throw new InvalidDataException($"SearchText '{name}' not found!");
            }

            var element = await _articleCategoryRepository.GetByNameAsync(name);
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
                throw new InvalidDataException($"SearchText '{element.Name}' already exist!");
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
                    throw new InvalidDataException($"SearchText '{element.Name}' already exist!");
                }
            }

            await _articleCategoryRepository.UpdateAsync(element.ToEntity());
        }

        public async Task DeleteAsync(string id)
        {
            if (await ExistByIdAsync(id) is false)
            {
                throw new InvalidDataException($"Id '{id}' not found!");
            }

            await _articleCategoryRepository.DeleteAsync(id);
        }
    }
}
