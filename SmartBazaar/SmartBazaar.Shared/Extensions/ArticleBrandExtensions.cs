using Mapster;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Extensions;

public static class ArticleBrandExtensions
{
    public static ArticleBrandCreateModel Adapt(this ArticleBrandCreateModel model, ArticleBrandDataModel dataToAppend)
    {
        dataToAppend.Adapt(model);
        return model;
    }

    public static ArticleBrandModel Adapt(this ArticleBrandModel model, ArticleBrandDataModel dataToAppend)
    {
        dataToAppend.Adapt(model);
        return model;
    }
}