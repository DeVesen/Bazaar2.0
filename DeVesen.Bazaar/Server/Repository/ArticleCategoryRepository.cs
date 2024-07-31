using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Contracts;
using DeVesen.Bazaar.Server.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Server.Repository;

[ExcludeFromCodeCoverage]
public class ArticleCategoryRepository : IArticleCategoryRepository
{
    private readonly List<ArticleCategoryEntity> _innerList = new()
    {
        new()
        {
            Id = Guid.Parse("D279BF98-C53B-478E-BAFB-D5659D5CFCED"),
            Name = "Helm"
        },
        new()
        {
            Id = Guid.Parse("D279BF98-C53B-478E-BAFB-D5659D5CFCED"),
            Name = "Schützer"
        },
        new()
        {
            Id = Guid.Parse("D279BF98-C53B-478E-BAFB-D5659D5CFCED"),
            Name = "Unterwäsche"
        },
        new()
        {
            Id = Guid.Parse("a1a1b2c3-d4d5-e6f7-g8h9-i0j1k2l3m4n5"),
            Name = "Bekleidung"
        },
        new()
        {
            Id = Guid.Parse("b2b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"),
            Name = "Schuhe"
        },
        new()
        {
            Id = Guid.Parse("c3c3d4e5-f6g7-h8i9-j0k1-l2m3n4o5p6q7"),
            Name = "Zubehör"
        },
        new()
        {
            Id = Guid.Parse("d4d4e5f6-g7h8-i9j0-k1l2-m3n4o5p6q7r8"),
            Name = "Rucksäcke"
        },
        new()
        {
            Id = Guid.Parse("e5e5f6g7-h8i9-j0k1-l2m3-n4o5p6q7r8s9"),
            Name = "Brillen"
        },
        new()
        {
            Id = Guid.Parse("f6f6g7h8-i9j0-k1l2-m3n4-o5p6q7r8s9t0"),
            Name = "Uhren"
        },
        new()
        {
            Id = Guid.Parse("g7g7h8i9-j0k1-l2m3-n4o5-p6q7r8s9t0u1"),
            Name = "Elektronik"
        }
    };

    public async Task<bool> ExistAsync(Guid id)
    {
        await Task.Delay(1);

        return _innerList.Any(p => p.Id == id);
    }

    public async Task<bool> ExistAsync(string name)
    {
        await Task.Delay(1);

        return _innerList.Any(p => p.Name == name);
    }

    public async Task<ArticleCategoryEntity> GetAsync(Guid id)
    {
        await Task.Delay(1);

        return _innerList.First(p => p.Id == id);
    }

    public async Task<ArticleCategoryEntity> GetAsync(string name)
    {
        await Task.Delay(1);

        return _innerList.First(p => p.Name == name);
    }

    public async Task<IEnumerable<ArticleCategoryEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return _innerList.AsEnumerable();
    }

    public async Task CreateAsync(ArticleCategoryEntity entity)
    {
        _innerList.Add(entity);

        await Task.Delay(100);
    }

    public async Task UpdateAsync(ArticleCategoryEntity entity)
    {
        var elementIndex = _innerList.FindIndex(p => p.Id == entity.Id);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException(ResourceText.Transform(ResourceText.ArticleCategory.NotFoundById, _ => entity.Id));
        }

        _innerList[elementIndex] = entity;

        await Task.Delay(100);
    }

    public async Task DeleteAsync(Guid id)
    {
        var element = _innerList.FirstOrDefault(p => p.Id == id);

        if (element == null)
        {
            throw new InvalidOperationException(ResourceText.Transform(ResourceText.ArticleCategory.NotFoundById, _ => id));
        }

        _innerList.Remove(element);

        await Task.Delay(100);
    }
}