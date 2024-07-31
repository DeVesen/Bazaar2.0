using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Tests.Fake.Repository;

[ExcludeFromCodeCoverage]
public class ArticleRepositoryFake : IArticleRepository
{
    public readonly List<ArticleEntity> InnerList;

    public ArticleRepositoryFake(params ArticleEntity[] elements)
    {
        InnerList = elements.ToList();
    }

    public async Task<bool> ExistAsync(string id)
    {
        await Task.Delay(1);

        return InnerList.Any(p => p.Id == id);
    }

    public async Task<bool> ExistAsync(long number)
    {
        await Task.Delay(1);

        return InnerList.Any(p => p.Number == number);
    }

    public async Task<ArticleEntity> GetAsync(string id)
    {
        await Task.Delay(1);

        return InnerList.First(p => p.Id == id);
    }

    public async Task<ArticleEntity> GetAsync(long number)
    {
        await Task.Delay(1);

        return InnerList.First(p => p.Number == number);
    }

    public async Task<IEnumerable<ArticleEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return InnerList.AsEnumerable();
    }

    public async Task CreateAsync(ArticleEntity entity)
    {
        InnerList.Add(entity);

        await Task.Delay(100);
    }

    public async Task UpdateAsync(ArticleEntity entity)
    {
        var elementIndex = InnerList.FindIndex(p => p.Id == entity.Id);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException($"Article '{entity.Id}' nicht gefunden!");
        }

        InnerList[elementIndex] = entity;

        await Task.Delay(100);
    }

    public async Task DeleteAsync(string id)
    {
        var element = InnerList.FirstOrDefault(p => p.Id == id);

        if (element == null)
        {
            throw new InvalidOperationException($"Article '{id}' nicht gefunden!");
        }

        InnerList.Remove(element);

        await Task.Delay(100);
    }
}