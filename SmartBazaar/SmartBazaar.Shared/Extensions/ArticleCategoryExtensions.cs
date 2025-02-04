using Mapster;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Extensions;

public static class ArticleCategoryExtensions
{
    public static ArticleCategoryCreateModel Adapt(this ArticleCategoryCreateModel model, ArticleCategoryDataModel dataToAppend)
    {
        dataToAppend.Adapt(model);
        return model;
    }

    public static ArticleCategoryModel Adapt(this ArticleCategoryModel model, ArticleCategoryDataModel dataToAppend)
    {
        dataToAppend.Adapt(model);
        return model;
    }
}