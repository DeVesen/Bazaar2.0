using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Validator;

public class ArticleModifyValidator : BaseValidator<ArticleDataModel>
{
    public ArticleModifyValidator(
        IArticleBrandService articleBrandService,
        IArticleCategoryService articleCategoryService)
    {
        RuleFor(item => item.Description)
            .SetValidator(new ArticleDescriptionValidator(articleBrandService, articleCategoryService));

        RuleFor(item => item.Price)
            .SetValidator(new ArticlePriceValidator());
    }
}