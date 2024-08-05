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
        return await _articleRepository.ExistByIdAsync(id);
    }

    public async Task<bool> ExistByNumberAsync(long number)
    {
        return await _articleRepository.ExistByNumberAsync(number);
    }

    public async Task<bool> ExistByNumberAsync(long number, string? allowedId)
    {
        return await _articleRepository.ExistByNumberAsync(number, allowedId);
    }

    public async Task<Article> GetByIdAsync(string id)
    {
        var element = await _articleRepository.TryGetByIdAsync(id);

        if (element.Exist is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        return element.Entity!.ToDomain();
    }

    public async Task<IEnumerable<VendorArticleStatistic>> GetStatisticPerVendor()
    {
        var articles = await GetAllAsync(new ArticleFilter());
        var groupedByVendor = articles.GroupBy(p => p.VendorId);

        return groupedByVendor.Select(p => new VendorArticleStatistic
        {
            VendorId = p.Key,
            NotOpen = p.Count(x => x.ApprovedForSale.HasValue is false),
            Open = p.Count(x => x.ApprovedForSale.HasValue),
            Sold = p.Count(x => x.Sold.HasValue),
            Settled = p.Count(x => x.Settled.HasValue),
            Turnover = p.Where(x => x.SoldAt.HasValue).Sum(y => y.SoldAt!.Value)
        });
    }

    public async Task<Article> GetByNumberAsync(long number)
    {
        var element = await _articleRepository.TryGetByNumberAsync(number);

        if (element.Exist is false)
        {
            throw new InvalidDataException($"Number '{number}' not found!");
        }

        return element.Entity!.ToDomain();
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

    public async Task DeleteByIdAsync(string id)
    {
        if (await ExistByIdAsync(id) is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        await _articleRepository.DeleteAsync(id);
    }

    public async Task DeleteByNumberAsync(long number)
    {
        var element = await _articleRepository.TryGetByNumberAsync(number);

        if (element.Exist is false)
        {
            throw new InvalidDataException($"Number '{number}' not found!");
        }

        await _articleRepository.DeleteAsync(element.Entity!.Id);
    }
}