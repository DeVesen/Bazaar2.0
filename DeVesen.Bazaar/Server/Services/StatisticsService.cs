using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared.Statistics;

namespace DeVesen.Bazaar.Server.Services;

public class StatisticsService(VendorStorage vendorStorage, ArticleStorage articleStorage)
{
    public CountsStatistics CalculateCounts(Article[] articles)
    {
        return new()
        {
            Count = articles.Length,
            Approved = articles.Count(p => p.IsApproved()),
            OnSale = articles.Count(p => p.IsOnSale()),
            Sold = articles.Count(p => p.IsSold()),
            Returned = articles.Count(p => p.IsReturned()),
            Settled = articles.Count(p => p.IsSettled())
        };
    }

    public ValuesStatistics CalculateValues(double offerUnitPrice, double salesShare, Article[] articles)
    {
        return new()
        {
            ApprovedValue = articles.Where(p => p.IsApproved())
                .Sum(p => p.Price01),
            SoldValue = articles.Where(p => p.IsSold())
                .Sum(p => p.Price01),
            OfferValue = articles.Count(p => p.IsApproved()) * offerUnitPrice,
            ShareValue = articles.Where(p => p.IsSold())
                .Sum(p => p.SoldAt!.Value) * salesShare
        };
    }
}