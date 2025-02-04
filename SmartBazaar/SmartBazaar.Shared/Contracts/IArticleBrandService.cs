using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Contracts;

public interface IArticleBrandService
{
    public Task<IResult> ExistByTitleAsync(string title);

    public Task<IResult<ArticleBrandModel>> GetByIdAsync(string articleCategoryId);
    public Task<IResult<ArticleBrandModel>> GetByTitleAsync(string title);
    public Task<IResult<ArticleBrandModel[]>> GetAllAsync(string? textSearchValue = null);

    public Task<IResult<ArticleBrandModel>> CreateAsync(string title);
    public Task<IResult<ArticleBrandModel>> CreateAsync(ArticleBrandCreateModel data);
    public Task<IResult<ArticleBrandModel>> UpdateAsync(ArticleBrandModel data);
    public Task<IResult> DeleteAsync(string id);
}