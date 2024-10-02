using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Extensions;
using DeVesen.Bazaar.Server.Hubs;
using DeVesen.Bazaar.Shared.Services;
using FluentResults;

namespace DeVesen.Bazaar.Server.Storage;

public class ArticleStorage
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly ArticleHubContext _articleHubContext;
    private readonly SystemClock _systemClock;

    public ArticleStorage(IVendorRepository vendorRepository, IArticleRepository articleRepository, ArticleHubContext articleHubContext, SystemClock systemClock)
    {
        _vendorRepository = vendorRepository;
        _articleRepository = articleRepository;
        _systemClock = systemClock;
        _articleHubContext = articleHubContext;
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
            elements = elements.Where(p => p.Number.ToString().Contains(articleFilter.Number.ToString()!));
        }
        if (articleFilter.SearchText != null)
        {
            elements = elements.Where(p => p.Description.ToLower().Contains(articleFilter.SearchText.ToLower()));
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
            throw new InvalidDataException($"SearchText '{element.Number}' already exist!");
        }
            
        if (await _vendorRepository.ExistByIdAsync(element.VendorId) is false)
        {
            throw new InvalidDataException($"Vendor not known by '{element.Id}'!");
        }

        await _articleRepository.CreateAsync(element.ToEntity());

        await _articleHubContext.SendAdded(element.VendorId, element.Id, element.Number);
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
                throw new InvalidDataException($"SearchText '{element.Number}' already exist!");
            }
        }

        if (await _vendorRepository.ExistByIdAsync(element.VendorId) is false)
        {
            throw new InvalidDataException($"Vendor not known by '{element.Id}'!");
        }

        await _articleRepository.UpdateAsync(element.ToEntity());

        await _articleHubContext.SendUpdated(element.VendorId, element.Id, element.Number);
    }

    public async Task DeleteByIdAsync(string id)
    {
        var response = await _articleRepository.TryGetByIdAsync(id);

        if (response.Exist is false)
        {
            throw new InvalidDataException($"Id '{id}' not found!");
        }

        await _articleRepository.DeleteAsync(id);

        await _articleHubContext.SendRemoved(response.Entity!.VendorId, response.Entity!.Id, response.Entity!.Number);
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

    public async Task BookOrderAsync(IEnumerable<SalesOrder.Position> positions)
    {
        var soldTime = _systemClock.GetNow();

        foreach (var item in positions)
        {
            var element = await GetByNumberAsync(item.Number);

            element.Sold = soldTime;
            element.SoldAt = item.Price;

            await UpdateAsync(element);

            await _articleHubContext.SendSold(element.VendorId, element.Id, element.Number);
        }
    }

    public async Task<Result> ApproveArticleAsync(long number, string vendorId)
    {
        if (await ExistByNumberAsync(number) is false)
        {
            return Result.Fail($"Artikel {number} nicht gefunden!");
        }

        var element = await GetByNumberAsync(number);

        if (element.VendorId != vendorId)
        {
            return Result.Fail($"Artikel {number} gehört nicht zu diesem Händler!");
        }

        if (element.Settled.HasValue)
        {
            return Result.Fail($"Artikel {number} ist bereits abgerechnet!");
        }

        if (element.Returned.HasValue)
        {
            return Result.Fail($"Artikel {number} ist zurückgegeben!");
        }

        if (element.SoldAt.HasValue)
        {
            return Result.Fail($"Artikel {number} wurde bereits verkauft!");
        }

        if (element.ApprovedForSale.HasValue)
        {
            return Result.Ok();
        }

        element.ApprovedForSale = _systemClock.GetNow();

        await UpdateAsync(element);

        await _articleHubContext.SendApproved(element.VendorId, element.Id, element.Number);

        return Result.Ok();
    }

    public async Task<Result> GiveBackArticleAsync(long number, string vendorId)
    {
        if (await ExistByNumberAsync(number) is false)
        {
            return Result.Fail($"Artikel {number} nicht gefunden!");
        }

        var element = await GetByNumberAsync(number);

        if (element.VendorId != vendorId)
        {
            return Result.Fail($"Artikel {number} gehört nicht zu diesem Händler!");
        }

        if (element.Settled.HasValue)
        {
            return Result.Fail($"Artikel {number} ist bereits abgerechnet!");
        }

        if (element.Returned.HasValue)
        {
            return Result.Fail($"Artikel {number} ist zurückgegeben!");
        }

        if (element.SoldAt.HasValue)
        {
            return Result.Fail($"Artikel {number} wurde bereits verkauft!");
        }

        if (element.ApprovedForSale.HasValue is false)
        {
            return Result.Fail($"Artikel {number} war noch nicht übernommen!");
        }

        if (element.Returned.HasValue)
        {
            return Result.Ok();
        }

        element.Returned = _systemClock.GetNow();

        await UpdateAsync(element);

        await _articleHubContext.SendReturned(element.VendorId, element.Id, element.Number);

        return Result.Ok();
    }

    public async Task<Result> SettleArticleAsync(params string[] actionArticleIds)
    {
        var elements = await GetAllAsync();
        elements = elements.Where(p => actionArticleIds.Contains(p.Id));

        foreach (var element in elements)
        {
            element.Settled = _systemClock.GetNow();

            await UpdateAsync(element);

            await _articleHubContext.SendReturned(element.VendorId, element.Id, element.Number);
        }

        return Result.Ok();
    }
}