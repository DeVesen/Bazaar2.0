using System.Diagnostics.CodeAnalysis;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;

namespace DeVesen.Bazaar.Server.Tests.Fake.Repository;

[ExcludeFromCodeCoverage]
public class ArticleCategoryRepositoryFake : IArticleCategoryRepository
{
    public readonly List<ArticleCategoryEntity> InnerList;

    public ArticleCategoryRepositoryFake(params ArticleCategoryEntity[] elements)
    {
        InnerList = elements.ToList();
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        await Task.Delay(1);

        return InnerList.Any(p => p.Id == id);
    }

    public async Task<bool> ExistAsync(string name)
    {
        await Task.Delay(1);

        return InnerList.Any(p => p.Name == name);
    }

    public async Task<ArticleCategoryEntity> GetAsync(Guid id)
    {
        await Task.Delay(1);

        return InnerList.First(p => p.Id == id);
    }

    public async Task<ArticleCategoryEntity> GetAsync(string name)
    {
        await Task.Delay(1);

        return InnerList.First(p => p.Name == name);
    }

    public async Task<IEnumerable<ArticleCategoryEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return InnerList.AsEnumerable();
    }

    public async Task CreateAsync(ArticleCategoryEntity entity)
    {
        InnerList.Add(entity);

        await Task.Delay(100);
    }

    public async Task UpdateAsync(ArticleCategoryEntity entity)
    {
        var elementIndex = InnerList.FindIndex(p => p.Id == entity.Id);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException($"Article-Category '{entity.Id}' nicht gefunden!");
        }

        InnerList[elementIndex] = entity;

        await Task.Delay(100);
    }

    public async Task DeleteAsync(Guid id)
    {
        var element = InnerList.FirstOrDefault(p => p.Id == id);

        if (element == null)
        {
            throw new InvalidOperationException($"Article-Category '{id}' nicht gefunden!");
        }

        InnerList.Remove(element);

        await Task.Delay(100);
    }
}