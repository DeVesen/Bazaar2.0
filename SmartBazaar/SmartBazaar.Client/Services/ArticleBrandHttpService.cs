using Fluxor;
using SmartBazaar.Client.Models;
using SmartBazaar.Client.States.ArticleBrandState;
using SmartBazaar.Shared;
using SmartBazaar.Shared.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartBazaar.Client.Services;

public class ArticleBrandHttpService(ArticleBrandFacade articleBrandFacade) : IArticleBrandService
{
    private readonly List<ArticleBrandDto> _innerList =
        [
            CreateElement("Head", "./Images/ArticleBrands/Head-logo.svg"),
            CreateElement("Atomic", "./Images/ArticleBrands/Atomic_logo.jpg"),
            CreateElement("K2", "./Images/ArticleBrands/K2_Sports_logo.png")
        ];


    public async Task<IResult> ExistByTitleAsync(string title)
        => await GetByTitleAsync(title);


    public Task<IResult<ArticleBrand>> GetByIdAsync(string id)
    {
        var result = _innerList.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        var domainElement = result != null ? MapToDomain(result) : null;

        return Task.FromResult(Result<ArticleBrand>.Create(domainElement != null, "Fehler beim holen", domainElement));
    }

    public Task<IResult<ArticleBrand>> GetByTitleAsync(string title)
    {
        var result = _innerList.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        var domainElement = result != null ? MapToDomain(result) : null;

        return Task.FromResult(Result<ArticleBrand>.Create(domainElement != null, "Fehler beim holen", domainElement));
    }

    public async Task<IResult<ArticleBrand[]>> GetAllAsync(string? textSearchValue = null)
    {
        var result = _innerList.AsEnumerable();

        if (string.IsNullOrWhiteSpace(textSearchValue) is false)
        {
            result = result.Where(p => p.Title.Contains(textSearchValue, StringComparison.OrdinalIgnoreCase));
        }

        return await Task.FromResult(Result<ArticleBrand[]>.Success(result.Select(MapToDomain).ToArray()));
    }


    public async Task<IResult<ArticleBrand>> CreateAsync(string title)
    {
        var data = new ArticleBrand
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            ImgSrc = string.Empty
        };

        return await CreateAsync(data);
    }

    public Task<IResult<ArticleBrand>> CreateAsync(ArticleBrand data)
    {
        var dtoElement = MapToDto(data);
        var domainElement = MapToDomain(dtoElement);

        _innerList.Add(dtoElement);

        articleBrandFacade.UpdateElement(data);

        return Task.FromResult(Result<ArticleBrand>.Success(domainElement));
    }


    public Task<IResult<ArticleBrand>> UpdateAsync(ArticleBrand data)
    {
        var dtoElement = MapToDto(data);
        var domainElement = MapToDomain(dtoElement);

        var index = _innerList.FindIndex(p => p.Id.Equals(data.Id, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
        {
            return Task.FromResult(Result<ArticleBrand>.Fail("Element not found!"));
        }

        _innerList[index] = dtoElement;

        articleBrandFacade.UpdateElement(data);

        return Task.FromResult(Result<ArticleBrand>.Success(domainElement));
    }

    public Task<IResult> DeleteAsync(string id)
    {
        _innerList.RemoveAll(p => p.Id == id);

        articleBrandFacade.RemoveElement(id);

        return Task.FromResult(Result.Success());
    }


    private static ArticleBrandDto MapToDto(ArticleBrand data)
        => new()
        {
            Id = data.Id,
            Title = data.Title,
            Url = data.ImgSrc ?? string.Empty
        };

    private static ArticleBrand MapToDomain(ArticleBrandDto data)
        => new()
        {
            Id = data.Id,
            Title = data.Title,
            ImgSrc = data.Url
        };


    private static ArticleBrandDto CreateElement(string title, string url)
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Url = url
        };
}

public interface IArticleBrandService
{
    public Task<IResult> ExistByTitleAsync(string title);


    public Task<IResult<ArticleBrand>> GetByIdAsync(string id);

    public Task<IResult<ArticleBrand>> GetByTitleAsync(string title);

    public Task<IResult<ArticleBrand[]>> GetAllAsync(string? textSearchValue = null);


    public Task<IResult<ArticleBrand>> CreateAsync(string title);

    public Task<IResult<ArticleBrand>> CreateAsync(ArticleBrand data);


    public Task<IResult<ArticleBrand>> UpdateAsync(ArticleBrand data);

    public Task<IResult> DeleteAsync(string id);
}