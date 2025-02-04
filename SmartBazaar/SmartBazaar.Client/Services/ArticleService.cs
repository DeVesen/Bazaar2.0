using SmartBazaar.Client.States.ArticleState;
using SmartBazaar.Client.States.DealerInfoState;
using SmartBazaar.Shared;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Extensions;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.Services;

public class ArticleService(DealerInfoFacade dealerInfoFacade, ArticleFacade articleFacade) : IArticleService
{
    private readonly List<ArticleModel> _innerList =
    [
        new ArticleModel
        {
            Id = Guid.NewGuid().ToString(),
            DealerId = "23E2903D-0D02-4632-85E5-374F62FE6BA5",
            Number = "123456",
            Description = new ArticleDescription
            {
                ArticleBrand = "Test Brand",
                ArticleCategory = "Test Category",
                Description = "This is a test article"
            },
            Price = new ArticlePrice
            {
                Price01 = 123.45,
                Price02 = 234.56
            }
        }
    ];


    public Task<IResult> ExistByNumberAsync(string value)
    {
        var found = _innerList.Any(p => p.Number.Equals(value, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(Result.Create(found, found ? string.Empty : "Item not found"));
    }


    public Task<IResult<ArticleModel>> GetByIdAsync(string articleId)
    {
        var result = _innerList.FirstOrDefault(p => p.Id.Equals(articleId, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(Result<ArticleModel>.Create(result != null, "Fehler beim holen", result));
    }

    public async Task<IResult<ArticleModel[]>> GetAllAsync()
    {
        var result = _innerList.Select(CopyModel).AsEnumerable();

        return await Task.FromResult(Result<ArticleModel[]>.Success(result.ToArray()));
    }


    public Task<IResult<ArticleModel>> CreateAsync(ArticleCreateModel data)
    {
        var article = new ArticleModel
        {
            Id = Guid.NewGuid().ToString(),
            DealerId = data.DealerId
        }.Adapt(data);

        _innerList.Add(article);

        dealerInfoFacade.ReLoadDealer(article.DealerId);
        articleFacade.UpdateElement(article);

        return Task.FromResult(Result<ArticleModel>.Success(article));
    }

    public Task<IResult<ArticleModel>> UpdateAsync(ArticleModel data)
    {
        var index = _innerList.FindIndex(p => p.Id.Equals(data.Id, StringComparison.OrdinalIgnoreCase));
        var newElement = CopyModel(data);

        if (index < 0)
        {
            return Task.FromResult(Result<ArticleModel>.Fail("Element not found!"));
        }

        _innerList[index] = newElement;

        dealerInfoFacade.ReLoadDealer(newElement.DealerId);
        articleFacade.UpdateElement(newElement);

        return Task.FromResult(Result<ArticleModel>.Success(newElement));
    }

    public Task<IResult> DeleteAsync(string id)
    {
        var relevantElements = _innerList.Where(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                                         .ToArray();

        foreach (var element in relevantElements)
        {
            _innerList.Remove(element);

            dealerInfoFacade.ReLoadDealer(element.DealerId);
            articleFacade.RemoveElement(element.Id);
        }

        return Task.FromResult(Result.Success());
    }


    public Task<IResult<string>> GetNextFreeNumber()
    {
        return Task.FromResult(Result<string>.Success(Guid.NewGuid().ToString()));
    }

    public Task<IResult<StatisticModel>> GetStatistics(string? dealerId)
    {
        var allElements = string.IsNullOrWhiteSpace(dealerId)
            ? _innerList.ToArray()
            : _innerList.Where(p => p.DealerId.Equals(dealerId, StringComparison.OrdinalIgnoreCase)).ToArray();

        var statistics = new StatisticModel
        {
            ArticleCount = allElements.Length,
            ArticlesApprovedCount = allElements.Count(p => p.Status.IsApprovedForSale()),
            ArticlesInSaleCount = allElements.Count(p => p.Status.IsInSale()),
            ArticlesSoldCount = allElements.Count(p => p.Status.IsSold()),
            ArticlesReturnedCount = allElements.Count(p => p.Status.IsReturned()),
            ArticlesSettledCount = allElements.Count(p => p.Status.IsSettled())
        };

        return Task.FromResult(Result<StatisticModel>.Success(statistics));
    }


    private static ArticleModel CopyModel(ArticleModel source)
    {
        return new ArticleModel
        {
            Id = source.Id,
            DealerId = source.DealerId,
            Number = source.Number,
            Description = CopyModel(source.Description),
            Price = CopyModel(source.Price),
            Status = CopyModel(source.Status)
        };
    }
    private static ArticleDescription CopyModel(ArticleDescription source)
    {
        return new ArticleDescription
        {
            ArticleBrand = source.ArticleBrand,
            ArticleCategory = source.ArticleCategory,
            Description = source.Description,
        };
    }
    private static ArticlePrice CopyModel(ArticlePrice source)
    {
        return new ArticlePrice
        {
            Price01 = source.Price01,
            Price02 = source.Price02
        };
    }
    private static ArticleStatus CopyModel(ArticleStatus source)
    {
        return new ArticleStatus
        {
            Created = source.Created,
            ApprovedForSale = source.ApprovedForSale,
            Sold = source.Sold,
            SoldAt = source.SoldAt,
            Returned = source.Returned,
            Settled = source.Settled
        };
    }
}