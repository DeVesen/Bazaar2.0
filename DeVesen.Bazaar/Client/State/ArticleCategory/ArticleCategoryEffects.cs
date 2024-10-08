﻿using DeVesen.Bazaar.Client.Extensions;
using DeVesen.Bazaar.Client.Services;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.ArticleCategory;

public class ArticleCategoryEffects(ArticleCategoryService articleCategoryService)
{
    [EffectMethod]
    public async Task FetchVendors(ArticleCategoryActions.Fetch action, IDispatcher dispatcher)
    {
        var response = await articleCategoryService.GetAllAsync(action.Name, action.SearchText);

        if (response.IsValid is false)
        {
            dispatcher.Dispatch(new ArticleCategoryActions.FetchFailed());
        }

        var domainElements = response.Value
            .OrderBy(p => p.Name)
            .Select(dtoElement => dtoElement.ToDomain())
            .ToArray();

        dispatcher.Dispatch(new ArticleCategoryActions.Set(domainElements));
    }
}