using Mapster;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Extensions;

public static class ArticleModelExtensions
{
    public static bool IsApprovedForSale(this ArticleStatus articleStatus) => articleStatus.ApprovedForSale.HasValue;

    public static bool IsSold(this ArticleStatus articleStatus) => articleStatus is { ApprovedForSale: not null, Sold: not null } &&
                                                                   articleStatus.Returned.HasValue is false;

    public static bool IsReturned(this ArticleStatus articleStatus) => articleStatus is { ApprovedForSale: not null, Returned: not null } &&
                                                                       articleStatus.Sold.HasValue is false;

    public static bool IsSettled(this ArticleStatus articleStatus) => articleStatus is { ApprovedForSale: not null, Settled: not null };

    public static bool IsInSale(this ArticleStatus articleStatus) => articleStatus.ApprovedForSale.HasValue &&
                                                                     articleStatus.Sold.HasValue is false &&
                                                                     articleStatus.Returned.HasValue is false &&
                                                                     articleStatus.Settled.HasValue is false;

    public static string GetStatusText(this ArticleStatus articleStatus)
    {
        if (articleStatus.IsApprovedForSale())
        {
            return "---";
        }

        if (articleStatus.IsSold())
        {
            return "Verkauft";
        }
        if (articleStatus.IsReturned())
        {
            return "Zurückgegeben";
        }
        if (articleStatus.IsSettled())
        {
            return "Abgerechnet";
        }

        return "Im Verkauf";
    }


    public static ArticleCreateModel Adapt(this ArticleCreateModel model, ArticleDataModel dataToAppend)
    {
        dataToAppend.Adapt(model);
        return model;
    }

    public static ArticleModel Adapt(this ArticleModel model, ArticleDataModel dataToAppend)
    {
        dataToAppend.Adapt(model);
        return model;
    }
}