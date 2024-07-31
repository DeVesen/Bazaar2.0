using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Shared.Basics;
using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Repository;

[ExcludeFromCodeCoverage]
public class ArticleRepository : IArticleRepository
{
    private readonly List<ArticleEntity> _innerList = new()
    {
        new()
        {
            Id = "DECB2A16-12DB-426C-8032-E7163530B16E".ToShortHash(),
            VendorId = "DECB2A16-12DB-426C-8032-E7163530B16E".ToShortHash(),
            Number = 1,
            Title = "Helm - Integral",
            ArticleCategory = "Ski-Helm",
            Manufacturer = "Atomic",
            Created = DateTime.Now,
            Price01 = 90
        },
        new()
        {
            Id = "F2F9E5D2-BD17-41D5-AFA8-B829B83F5439".ToShortHash(),
            VendorId = "F2F9E5D2-BD17-41D5-AFA8-B829B83F5439".ToShortHash(),
            Number = 2,
            Title = "Ski Erwachsene",
            ArticleCategory = "Ski",
            Manufacturer = "K2",
            Created = DateTime.Now - TimeSpan.FromHours(4),
            Price01 = 90,
            ApprovedForSale = DateTime.Now,
        },
        new()
        {
            Id = "95EB0C08-6D5F-4042-A0AC-301CF4BEDAFD".ToShortHash(),
            VendorId = "95EB0C08-6D5F-4042-A0AC-301CF4BEDAFD".ToShortHash(),
            Number = 3,
            Title = "Grüne Ski-Schuhe",
            ArticleCategory = "Ski-Schuhe",
            Manufacturer = "HEAD",
            Created = DateTime.Now - TimeSpan.FromHours(3),
            Price01 = 90,
            ApprovedForSale = DateTime.Now - TimeSpan.FromHours(1),
            Sold = DateTime.Now,
        }
    };

    public async Task<bool> ExistAsync(string id)
    {
        await Task.Delay(1);

        return _innerList.Any(p => p.Id == id);
    }

    public async Task<bool> ExistAsync(long number)
    {
        await Task.Delay(1);

        return _innerList.Any(p => p.Number == number);
    }

    public async Task<ArticleEntity> GetAsync(string id)
    {
        await Task.Delay(1);

        return _innerList.First(p => p.Id == id);
    }

    public async Task<ArticleEntity> GetAsync(long number)
    {
        await Task.Delay(1);

        return _innerList.First(p => p.Number == number);
    }

    public async Task<IEnumerable<ArticleEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return _innerList.AsEnumerable();
    }

    public async Task CreateAsync(ArticleEntity entity)
    {
        _innerList.Add(entity);

        await Task.Delay(100);
    }

    public async Task UpdateAsync(ArticleEntity entity)
    {
        var elementIndex = _innerList.FindIndex(p => p.Id == entity.Id);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException(ResourceText.Transform(ResourceText.Article.NotFoundById, _ => entity.Id));
        }

        _innerList[elementIndex] = entity;

        await Task.Delay(100);
    }

    public async Task DeleteAsync(string id)
    {
        var element = _innerList.FirstOrDefault(p => p.Id == id);

        if (element == null)
        {
            throw new InvalidOperationException(ResourceText.Transform(ResourceText.Article.NotFoundById, _ => id));
        }

        _innerList.Remove(element);

        await Task.Delay(100);
    }
}