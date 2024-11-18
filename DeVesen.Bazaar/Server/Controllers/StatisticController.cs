using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Storage;
using Microsoft.AspNetCore.Mvc;

namespace DeVesen.Bazaar.Server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StatisticController(VendorStorage vendorStorage, ArticleStorage articleStorage) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var vendors = (await vendorStorage.GetAllAsync(new VendorFilter())).ToArray();
        var articles = (await articleStorage.GetAllAsync(new ArticleFilter())).ToArray();

        var vendorArticleView = vendors.GroupJoin(
            articles,
            vendor => vendor.Id,
            article => article.VendorId,
            (vendor, vendorArticles) => new
            {
                Vendor = vendor,
                Articles = vendorArticles.ToArray()
            });

        var elementView = vendorArticleView.SelectMany(item =>
        {
            return item.Articles.Select(article => new
            {
                IsApprovedForSale = article.ApprovedForSale.HasValue,

                IsOnSale = article.ApprovedForSale.HasValue &&
                           article.Sold.HasValue is false &&
                           article.Returned.HasValue is false &&
                           article.Settled.HasValue is false,

                IsSold = article.ApprovedForSale.HasValue &&
                         article.Sold.HasValue &&
                         article.Returned.HasValue is false &&
                         article.Settled.HasValue is false,

                IsReturned = article.ApprovedForSale.HasValue &&
                             article.Sold.HasValue is false &&
                             article.Returned.HasValue &&
                             article.Settled.HasValue is false,

                IsSettled = article.Settled.HasValue,

                item.Vendor.OfferUnitPrice,
                item.Vendor.SalesShare,

                Price = article.Price01,
                SoldFor = article.SoldAt ?? 0.0d
            });
        }).ToArray();

        var turnover = elementView.Sum(article => article.SoldFor);
        var turnoverClubShare = elementView.Sum(article => article.SoldFor * article.SalesShare);
        var turnoverVendorShare = turnover - turnoverClubShare;
        var workingFee = elementView.Where(p => p.IsApprovedForSale && p.IsReturned)
                                    .Sum(article => article.OfferUnitPrice);

        var supplyGraphData = articles.Where(p => p.ApprovedForSale.HasValue)
                                      .Select(p => new { Title = p.ApprovedForSale!.Value.ToString("yyyy-MM-dd HH") })
                                      .GroupBy(p => p.Title, (title, items) => new { Title = title, Count = items.Count() })
                                      .ToArray();
        var salesGraphData = articles.Where(p => p.Sold.HasValue)
                                     .Select(p => new { Title = p.Sold!.Value.ToString("yyyy-MM-dd HH") })
                                     .GroupBy(p => p.Title, (title, items) => new { Title = title, Count = items.Count() })
                                     .ToArray();

        var result = new
        {
            Vendors = new
            {
                TotalCount = vendors.Length
            },

            Articles = new
            {
                ApprovedCount = elementView.Count(p => p.IsApprovedForSale),
                ApprovedValue = elementView.Where(p => p.IsApprovedForSale).Sum(p => p.Price),
                TotalCount = elementView.Length,
                TotalValue = elementView.Sum(article => article.Price)
            },

            ActualStock = new
            {
                IsOnSaleCount = elementView.Count(p => p.IsOnSale),
                IsSoldCount = elementView.Count(p => p.IsSold),
                IsReturnedCount = elementView.Count(p => p.IsReturned),
                IsSettledCount = elementView.Count(p => p.IsSettled),
            },

            Income = new
            {
                Turnover = turnover,
                TurnoverVendorShare = turnoverVendorShare,
                TurnoverClubShare = turnoverClubShare,
                WorkingFee = workingFee
            },

            OffersGraphData = supplyGraphData,
            SalesGraphData = salesGraphData,
        };

        return Ok(result);
    }
}