using SmartBazaar.Client.States.ArticleCategoryState;
using SmartBazaar.Shared;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Extensions;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.Services;

public class ArticleCategoryService(ArticleCategoryFacade articleCategoryFacade) : IArticleCategoryService
{
    private readonly List<ArticleCategoryModel> _innerList =
    [
        CreateElement("Head", "./Images/ArticleBrands/Head-logo.svg"),
        CreateElement("Atomic", "./Images/ArticleBrands/Atomic_logo.jpg"),
        CreateElement("K2", "./Images/ArticleBrands/K2_Sports_logo.png")
    ];


    public async Task<IResult> ExistByTitleAsync(string title)
        => await GetByTitleAsync(title);

    public Task<IResult<ArticleCategoryModel>> GetByIdAsync(string articleCategoryId)
    {
        var result = _innerList.FirstOrDefault(p => p.Id.Equals(articleCategoryId, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(Result<ArticleCategoryModel>.Create(result != null, "Fehler beim holen", result));
    }

    public Task<IResult<ArticleCategoryModel>> GetByTitleAsync(string title)
    {
        var result = _innerList.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(Result<ArticleCategoryModel>.Create(result != null, "Fehler beim holen", result));
    }
    
    public async Task<IResult<ArticleCategoryModel[]>> GetAllAsync(string? textSearchValue = null)
    {
        var result = _innerList.AsEnumerable();

        if (string.IsNullOrWhiteSpace(textSearchValue) is false)
        {
            result = result.Where(p => p.Title.Contains(textSearchValue, StringComparison.OrdinalIgnoreCase));
        }

        return await Task.FromResult(Result<ArticleCategoryModel[]>.Success(result.ToArray()));
    }


    public async Task<IResult<ArticleCategoryModel>> CreateAsync(string title)
    {
        var data = new ArticleCategoryCreateModel
        {
            Title = title,
            ImgSrc = string.Empty
        };

        return await CreateAsync(data);
    }

    public Task<IResult<ArticleCategoryModel>> CreateAsync(ArticleCategoryCreateModel data)
    {
        var articleCategory = new ArticleCategoryModel
        {
            Id = Guid.NewGuid().ToString(),

        }.Adapt(data);

        _innerList.Add(articleCategory);

        articleCategoryFacade.UpdateElement(articleCategory);

        return Task.FromResult(Result<ArticleCategoryModel>.Success(articleCategory));
    }

    public Task<IResult<ArticleCategoryModel>> UpdateAsync(ArticleCategoryModel data)
    {
        var index = _innerList.FindIndex(p => p.Id.Equals(data.Id, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
        {
            return Task.FromResult(Result<ArticleCategoryModel>.Fail("Element not found!"));
        }

        _innerList[index] = data;

        articleCategoryFacade.UpdateElement(data);

        return Task.FromResult(Result<ArticleCategoryModel>.Success(data));
    }

    public Task<IResult> DeleteAsync(string id)
    {
        _innerList.RemoveAll(p => p.Id == id);

        articleCategoryFacade.RemoveElement(id);

        return Task.FromResult(Result.Success());
    }


    private static ArticleCategoryModel CreateElement(string title, string url)
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            ImgSrc = url
        };
}