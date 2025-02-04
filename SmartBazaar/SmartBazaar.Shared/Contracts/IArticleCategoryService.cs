using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Contracts;

public interface IArticleCategoryService
{
    public Task<IResult> ExistByTitleAsync(string title);

    public Task<IResult<ArticleCategoryModel>> GetByIdAsync(string articleCategoryId);
    public Task<IResult<ArticleCategoryModel>> GetByTitleAsync(string title);
    public Task<IResult<ArticleCategoryModel[]>> GetAllAsync(string? textSearchValue = null);

    public Task<IResult<ArticleCategoryModel>> CreateAsync(string title);
    public Task<IResult<ArticleCategoryModel>> CreateAsync(ArticleCategoryCreateModel data);
    public Task<IResult<ArticleCategoryModel>> UpdateAsync(ArticleCategoryModel data);
    public Task<IResult> DeleteAsync(string id);
}