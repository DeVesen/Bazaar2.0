using SmartBazaar.Shared;

namespace SmartBazaar.Client.Services;

public class ArticleHttpService : IArticleService
{
    public Task<IResult<string>> GetNextFreeNumber()
    {
        return Task.FromResult(Result<string>.Success(Guid.NewGuid().ToString()));
    }
}

public interface IArticleService
{
    public Task<IResult<string>> GetNextFreeNumber();
}