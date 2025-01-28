using SmartBazaar.Client.Models;
using SmartBazaar.Client.States.ArticleCategoryState;
using SmartBazaar.Shared;
using SmartBazaar.Shared.Dto;

namespace SmartBazaar.Client.Services;

public class ArticleCategoryHttpService : IArticleCategoryService
{
    private readonly ArticleCategoryFacade _articleCategoryFacade;
    private readonly List<ArticleCategoryDto> _innerList;

    public ArticleCategoryHttpService(ArticleCategoryFacade articleCategoryFacade)
    {
        _articleCategoryFacade = articleCategoryFacade;

        _innerList =
        [
            CreateElement("Head", "./Images/ArticleBrands/Head-logo.svg"),
            CreateElement("Atomic", "./Images/ArticleBrands/Atomic_logo.jpg"),
            CreateElement("K2", "./Images/ArticleBrands/K2_Sports_logo.png")
        ];
    }


    public async Task<IResult> ExistByTitleAsync(string title)
        => await GetByTitleAsync(title);


    public Task<IResult<ArticleCategory>> GetByIdAsync(string id)
    {
        var result = _innerList.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        var domainElement = result != null ? MapToDomain(result) : null;

        return Task.FromResult(Result<ArticleCategory>.Create(domainElement != null, "Fehler beim holen", domainElement));
    }

    public Task<IResult<ArticleCategory>> GetByTitleAsync(string title)
    {
        var result = _innerList.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        var domainElement = result != null ? MapToDomain(result) : null;

        return Task.FromResult(Result<ArticleCategory>.Create(domainElement != null, "Fehler beim holen", domainElement));
    }

    public async Task<IResult<ArticleCategory[]>> GetAllAsync(string? textSearchValue = null)
    {
        var result = _innerList.AsEnumerable();

        if (string.IsNullOrWhiteSpace(textSearchValue) is false)
        {
            result = result.Where(p => p.Title.Contains(textSearchValue, StringComparison.OrdinalIgnoreCase));
        }

        return await Task.FromResult(Result<ArticleCategory[]>.Success(result.Select(MapToDomain).ToArray()));
    }


    public async Task<IResult<ArticleCategory>> CreateAsync(string title)
    {
        var data = new ArticleCategory
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            ImgSrc = string.Empty
        };

        return await CreateAsync(data);
    }

    public Task<IResult<ArticleCategory>> CreateAsync(ArticleCategory data)
    {
        var dtoElement = MapToDto(data);
        var domainElement = MapToDomain(dtoElement);

        _innerList.Add(dtoElement);

        _articleCategoryFacade.UpdateElement(data);

        return Task.FromResult(Result<ArticleCategory>.Success(domainElement));
    }


    public Task<IResult<ArticleCategory>> UpdateAsync(ArticleCategory data)
    {
        var dtoElement = MapToDto(data);
        var domainElement = MapToDomain(dtoElement);

        var index = _innerList.FindIndex(p => p.Id.Equals(data.Id, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
        {
            return Task.FromResult(Result<ArticleCategory>.Fail("Element not found!"));
        }

        _innerList[index] = dtoElement;

        _articleCategoryFacade.UpdateElement(data);

        return Task.FromResult(Result<ArticleCategory>.Success(domainElement));
    }

    public Task<IResult> DeleteAsync(string id)
    {
        _innerList.RemoveAll(p => p.Id == id);

        _articleCategoryFacade.RemoveElement(id);

        return Task.FromResult(Result.Success());
    }


    private static ArticleCategoryDto MapToDto(ArticleCategory data)
        => new()
        {
            Id = data.Id,
            Title = data.Title,
            Url = data.ImgSrc ?? string.Empty
        };

    private static ArticleCategory MapToDomain(ArticleCategoryDto data)
        => new()
        {
            Id = data.Id,
            Title = data.Title,
            ImgSrc = data.Url
        };


    private static ArticleCategoryDto CreateElement(string title, string url)
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Url = url
        };
}

public interface IArticleCategoryService
{
    public Task<IResult> ExistByTitleAsync(string title);


    public Task<IResult<ArticleCategory>> GetByIdAsync(string id);

    public Task<IResult<ArticleCategory>> GetByTitleAsync(string title);

    public Task<IResult<ArticleCategory[]>> GetAllAsync(string? textSearchValue = null);


    public Task<IResult<ArticleCategory>> CreateAsync(string title);

    public Task<IResult<ArticleCategory>> CreateAsync(ArticleCategory data);


    public Task<IResult<ArticleCategory>> UpdateAsync(ArticleCategory data);

    public Task<IResult> DeleteAsync(string id);
}