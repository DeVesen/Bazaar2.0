using SmartBazaar.Client.States.ArticleBrandState;
using SmartBazaar.Shared;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Extensions;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.Services;

public class ArticleBrandService(ArticleBrandFacade articleBrandFacade) : IArticleBrandService
{
    private readonly List<ArticleBrandModel> _innerList =
        [
            CreateElement("Head", "./Images/ArticleBrands/Head-logo.svg"),
            CreateElement("Atomic", "./Images/ArticleBrands/Atomic_logo.jpg"),
            CreateElement("K2", "./Images/ArticleBrands/K2_Sports_logo.png")
        ];


    public async Task<IResult> ExistByTitleAsync(string title)
        => await GetByTitleAsync(title);

    public Task<IResult<ArticleBrandModel>> GetByIdAsync(string articleBrandId)
    {
        var result = _innerList.FirstOrDefault(p => p.Id.Equals(articleBrandId, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(Result<ArticleBrandModel>.Create(result != null, "Fehler beim holen", result));
    }

    public Task<IResult<ArticleBrandModel>> GetByTitleAsync(string title)
    {
        var result = _innerList.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(Result<ArticleBrandModel>.Create(result != null, "Fehler beim holen", result));
    }

    public async Task<IResult<ArticleBrandModel[]>> GetAllAsync(string? textSearchValue = null)
    {
        var result = _innerList.AsEnumerable();

        if (string.IsNullOrWhiteSpace(textSearchValue) is false)
        {
            result = result.Where(p => p.Title.Contains(textSearchValue, StringComparison.OrdinalIgnoreCase));
        }

        return await Task.FromResult(Result<ArticleBrandModel[]>.Success(result.ToArray()));
    }


    public async Task<IResult<ArticleBrandModel>> CreateAsync(string title)
    {
        var data = new ArticleBrandCreateModel
        {
            Title = title,
            ImgSrc = string.Empty
        };

        return await CreateAsync(data);
    }

    public Task<IResult<ArticleBrandModel>> CreateAsync(ArticleBrandCreateModel data)
    {
        var articleBrand = new ArticleBrandModel
        {
            Id = Guid.NewGuid().ToString(),
            
        }.Adapt(data);

        _innerList.Add(articleBrand);

        articleBrandFacade.UpdateElement(articleBrand);

        return Task.FromResult(Result<ArticleBrandModel>.Success(articleBrand));
    }

    public Task<IResult<ArticleBrandModel>> UpdateAsync(ArticleBrandModel data)
    {
        var index = _innerList.FindIndex(p => p.Id.Equals(data.Id, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
        {
            return Task.FromResult(Result<ArticleBrandModel>.Fail("Element not found!"));
        }

        _innerList[index] = data;

        articleBrandFacade.UpdateElement(data);

        return Task.FromResult(Result<ArticleBrandModel>.Success(data));
    }

    public Task<IResult> DeleteAsync(string id)
    {
        _innerList.RemoveAll(p => p.Id == id);

        articleBrandFacade.RemoveElement(id);

        return Task.FromResult(Result.Success());
    }


    private static ArticleBrandModel CreateElement(string title, string url)
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            ImgSrc = url
        };
}