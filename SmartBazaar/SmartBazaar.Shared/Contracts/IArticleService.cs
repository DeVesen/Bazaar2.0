using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Contracts;

public interface IArticleService
{
    public Task<IResult> ExistByNumberAsync(string value);

    public Task<IResult<ArticleModel>> GetByIdAsync(string articleId);
    public Task<IResult<ArticleModel[]>> GetAllAsync();
    
    public Task<IResult<ArticleModel>> CreateAsync(ArticleCreateModel data);
    public Task<IResult<ArticleModel>> UpdateAsync(ArticleModel data);
    public Task<IResult> DeleteAsync(string id);
    
    public Task<IResult<string>> GetNextFreeNumber();
    public Task<IResult<StatisticModel>> GetStatistics(string? dealerId);
}