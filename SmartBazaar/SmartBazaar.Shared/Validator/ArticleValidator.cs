using FluentValidation;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Validator;

public class ArticleCreateValidator : BaseValidator<ArticleDataModel>
{
    private readonly IDealerService _dealerService;
    private readonly IArticleService _articleService;

    public ArticleCreateValidator(
        IArticleBrandService articleBrandService,
        IArticleCategoryService articleCategoryService,
        IDealerService dealerService,
        IArticleService articleService)
    {
        _dealerService = dealerService;
        _articleService = articleService;

        RuleFor(item => item.DealerId)
            .NotEmpty().WithMessage("DealerId ist erforderlich!")
            .MustAsync(DealerKnownAsync).WithMessage("DealerId existiert nicht!");

        RuleFor(item => item.Number)
            .NotEmpty().WithMessage("Nummer ist erforderlich!")
            .MaximumLength(10).WithMessage("Number darf nicht größer {MaxLength} Zeichen sein!")
            .MustAsync(NumberNotKnownAsync).WithMessage("Titel existiert bereits!");

        Include(new ArticleModifyValidator(articleBrandService, articleCategoryService));
    }

    private async Task<bool> DealerKnownAsync(ArticleDataModel item, string value, CancellationToken _)
        => (await _dealerService.ExistByIdAsync(value)).IsSuccess;

    private async Task<bool> NumberNotKnownAsync(ArticleDataModel item, string value, CancellationToken _)
        => (await _articleService.ExistByNumberAsync(value)).IsSuccess is false;
}