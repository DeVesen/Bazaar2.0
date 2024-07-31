using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;

namespace DeVesen.Bazaar.Server.Storage;

public class ArticleStorage
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IArticleRepository _articleRepository;

    public ArticleStorage(IVendorRepository vendorRepository, IArticleRepository articleRepository)
    {
        _vendorRepository = vendorRepository;
        _articleRepository = articleRepository;
    }

    public async Task<bool> ExistByIdAsync(string id)
    {
        return await _articleRepository.ExistAsync(id);
    }

    public async Task<bool> ExistByNumberAsync(long number)
    {
        return await _articleRepository.ExistAsync(number);
    }

    public async Task<Article> GetByNumberAsync(long number)
    {
        var element = await _articleRepository.GetAsync(number);
        return element.ToDomain();
    }

    public async Task<IEnumerable<Article>> GetAllAsync()
    {
        return await GetAllAsync(new ArticleFilter());
    }

    public async Task<IEnumerable<Article>> GetAllAsync(ArticleFilter articleFilter)
    {
        var elements = await _articleRepository.GetAllAsync();

        if (articleFilter.VendorId != null)
        {
            elements = elements.Where(p => p.VendorId == articleFilter.VendorId);
        }
        if (articleFilter.Number != null)
        {
            elements = elements.Where(p => p.Number == articleFilter.Number);
        }
        if (articleFilter.Title != null)
        {
            elements = elements.Where(p => p.Title.ToLower().Contains(articleFilter.Title.ToLower()));
        }
        if (articleFilter.ArticleCategory != null)
        {
            elements = elements.Where(p => p.ArticleCategory == articleFilter.ArticleCategory);
        }
        if (articleFilter.Manufacturer != null)
        {
            elements = elements.Where(p => p.Manufacturer == articleFilter.Manufacturer);
        }

        return elements.Select(p => p.ToDomain());
    }

    public async Task CreateAsync(Article element)
    {
        if (await ExistByIdAsync(element.Id))
        {
            throw new InvalidDataException($"Id '{element.Id}' already exist!");
        }
        if (await ExistByNumberAsync(element.Number))
        {
            throw new InvalidDataException($"Name '{element.Number}' already exist!");
        }
            
        if (await _vendorRepository.ExistByIdAsync(element.VendorId) is false)
        {
            throw new InvalidDataException($"Vendor not known by '{element.Id}'!");
        }

        await _articleRepository.CreateAsync(element.ToEntity());
    }

    public async Task UpdateAsync(Article element)
    {
        if (await ExistByIdAsync(element.Id) is false)
        {
            throw new InvalidDataException($"Id '{element.Id}' not found!");
        }
        if (await ExistByNumberAsync(element.Number))
        {
            var entity = await GetByNumberAsync(element.Number);

            if (entity != null && entity.Id != element.Id)
            {
                throw new InvalidDataException($"Name '{element.Number}' already exist!");
            }
        }

        if (await _vendorRepository.ExistByIdAsync(element.VendorId) is false)
        {
            throw new InvalidDataException($"Vendor not known by '{element.Id}'!");
        }

        await _articleRepository.UpdateAsync(element.ToEntity());
    }

    public async Task DeleteAsync(string id)
    {
        if (await ExistByIdAsync(id) is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        await _articleRepository.DeleteAsync(id);
    }
}