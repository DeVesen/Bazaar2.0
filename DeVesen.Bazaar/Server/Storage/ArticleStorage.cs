using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Hubs;
using DeVesen.Bazaar.Shared.Events;
using DeVesen.Bazaar.Shared.Services;

namespace DeVesen.Bazaar.Server.Storage;

public class ArticleStorage
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly VendorHubContext _vendorHubContext;
    private readonly ArticleHubContext _articleHubContext;
    private readonly SystemClock _systemClock;


    public ArticleStorage(IVendorRepository vendorRepository, IArticleRepository articleRepository, VendorHubContext vendorHubContext, ArticleHubContext articleHubContext, SystemClock systemClock)
    {
        _vendorRepository = vendorRepository;
        _articleRepository = articleRepository;
        _systemClock = systemClock;
        _articleHubContext = articleHubContext;
        _vendorHubContext = vendorHubContext;
    }


    public async Task<bool> ExistByIdAsync(string id)
    {
        return await _articleRepository.ExistByIdAsync(id);
    }

    public async Task<bool> ExistByNumberAsync(long number)
    {
        return await _articleRepository.ExistByNumberAsync(number);
    }


    public async Task<(bool Found, Article Article)> TryGetByIdAsync(string id)
    {
        var element = await _articleRepository.TryGetByIdAsync(id);
        return element.Exist
            ? (true, element.Entity!.ToDomain())
            : (false, null!);
    }

    public async Task<(bool Found, Article Article)> TryGetByNumberAsync(long number)
    {
        var element = await _articleRepository.TryGetByNumberAsync(number);
        return element.Exist
            ? (true, element.Entity!.ToDomain())
            : (false, null!);
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
            elements = elements.Where(p => p.Number.ToString().Contains(articleFilter.Number!));
        }
        if (articleFilter.SearchText != null)
        {
            elements = elements.Where(p => p.Description.ToLower().Contains(articleFilter.SearchText.ToLower()));
        }

        return elements.Select(p => p.ToDomain());
    }


    public async Task<(bool IsSuccess, string Error)> CreateAsync(Article element)
    {
        var validationResult = await ValidateCreate(element);
        if (validationResult.IsSuccess is false)
        {
            return (false, validationResult.Error);
        }

        await _articleRepository.CreateAsync(element.ToEntity());

        await _articleHubContext.SendAdded(element);

        _vendorHubContext.SendUpdated(element.VendorId, VendorUpdatedArgs.Reasons.ArticleData);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string Error)> UpdateAsync(Article element)
    {
        var validationResult = await ValidateUpdate(element);
        if (validationResult.IsSuccess is false)
        {
            return (false, validationResult.Error);
        }

        await _articleRepository.UpdateAsync(element.ToEntity());

        await _articleHubContext.SendUpdated(element);

        _vendorHubContext.SendUpdated(element.VendorId, VendorUpdatedArgs.Reasons.ArticleData);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string Error)> DeleteByIdAsync(string id)
    {
        var result = await TryGetByIdAsync(id);

        if (result.Found is false)
        {
            return (false, $"Artikel '{id}' nicht bekannt!");
        }

        await _articleRepository.DeleteAsync(id);

        await _articleHubContext.SendRemoved(result.Article);

        _vendorHubContext.SendUpdated(result.Article.VendorId, VendorUpdatedArgs.Reasons.ArticleData);

        return (true, string.Empty);
    }


    public async Task<(bool IsSuccess, string Error)> BookOrderAsync(IEnumerable<SalesOrder.Position> positions)
    {
        var soldTime = _systemClock.GetNow();

        foreach (var item in positions)
        {
            var result = await TryGetByNumberAsync(item.Number);

            if (result.Found is false)
            {
                return (false, $"Artikel '{item.Number}' nicht bekannt!");
            }

            result.Article.Sold = soldTime;
            result.Article.SoldAt = item.Price;

            await UpdateAsync(result.Article);

            await _articleHubContext.SendSold(result.Article);
        }

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string Error)> ApproveArticleAsync(long number, string vendorId)
    {
        var result = await TryGetByNumberAsync(number);

        if (result.Found is false)
        {
            return (false, $"Artikel '{number}' nicht bekannt!");
        }

        if (result.Article.VendorId != vendorId)
        {
            return (false, $"Artikel {number} gehört nicht zu diesem Händler!");
        }

        if (result.Article.Settled.HasValue)
        {
            return (false, $"Artikel {number} ist bereits abgerechnet!");
        }

        if (result.Article.Returned.HasValue)
        {
            return (false, $"Artikel {number} ist zurückgegeben!");
        }

        if (result.Article.SoldAt.HasValue)
        {
            return (false, $"Artikel {number} wurde bereits verkauft!");
        }

        if (result.Article.ApprovedForSale.HasValue)
        {
            return (true, string.Empty);
        }

        result.Article.ApprovedForSale = _systemClock.GetNow();

        await UpdateAsync(result.Article);

        await _articleHubContext.SendApproved(result.Article);

        return (true, string.Empty);
    }

    public async Task<(bool IsSuccess, string Error)> GiveBackArticleAsync(long number, string vendorId)
    {
        var result = await TryGetByNumberAsync(number);

        if (result.Found is false)
        {
            return (false, $"Artikel '{number}' nicht bekannt!");
        }

        if (result.Article.VendorId != vendorId)
        {
            return (false, $"Artikel {number} gehört nicht zu diesem Händler!");
        }

        if (result.Article.Settled.HasValue)
        {
            return (false, $"Artikel {number} ist bereits abgerechnet!");
        }

        if (result.Article.Returned.HasValue)
        {
            return (false, $"Artikel {number} ist zurückgegeben!");
        }

        if (result.Article.SoldAt.HasValue)
        {
            return (false, $"Artikel {number} wurde bereits verkauft!");
        }

        if (result.Article.ApprovedForSale.HasValue is false)
        {
            return (false, $"Artikel {number} war noch nicht übernommen!");
        }

        if (result.Article.Returned.HasValue)
        {
            return (true, string.Empty);
        }

        result.Article.Returned = _systemClock.GetNow();

        await UpdateAsync(result.Article);

        await _articleHubContext.SendReturned(result.Article);

        return (true, string.Empty);
    }

    public async Task SettleArticlesAsync(string vendorId)
    {
        var elements = await GetAllAsync(new ArticleFilter { VendorId = vendorId });

        foreach (var element in elements.Where(p => p.IsSettled() is false))
        {
            element.Settled = _systemClock.GetNow();

            await UpdateAsync(element);

            await _articleHubContext.SendReturned(element);
        }
    }


    private async Task<(bool IsSuccess, string Error)> ValidateCreate(Article element)
    {
        if (await ExistByIdAsync(element.Id))
        {
            return (false, $"Artikel-Id '{element.Id}' existiert bereits!");
        }

        if (await ArticleNumberAlreadyInUsedByOther(element.Number))
        {
            return (false, $"Artikel-Nummer '{element.Number}' existiert bereits!");
        }

        if (await _vendorRepository.ExistByIdAsync(element.VendorId) is false)
        {
            return (false, $"Verkäufer '{element.Id}' nicht bekannt!");
        }

        return (true, string.Empty);
    }

    private async Task<(bool IsSuccess, string Error)> ValidateUpdate(Article element)
    {
        if (await ExistByIdAsync(element.Id) is false)
        {
            return (false, $"Artikel '{element.Id}' nicht bekannt!");
        }

        if (await ArticleNumberAlreadyInUsedByOther(element.Number, element.Id))
        {
            return (false, $"Artikel-Nummer '{element.Number}' existiert bereits!");
        }

        if (await _vendorRepository.ExistByIdAsync(element.VendorId) is false)
        {
            throw new InvalidDataException($"Verkäufer '{element.Id}' nicht bekannt!");
        }

        return (true, string.Empty);
    }


    private async Task<bool> ArticleNumberAlreadyInUsedByOther(long number, string? ownId = null)
    {
        var resultByNumber = await TryGetByNumberAsync(number);

        if (!resultByNumber.Found)
        {
            return false;
        }

        return resultByNumber.Article.Id != ownId;
    }
}