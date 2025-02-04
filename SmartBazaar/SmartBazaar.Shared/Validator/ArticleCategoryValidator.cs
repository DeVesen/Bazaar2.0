using FluentValidation;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Validator;

public class ArticleCategoryValidator : BaseValidator<ArticleCategoryDataModel>
{
    private readonly IArticleCategoryService _articleCategoryService;

    public ArticleCategoryValidator(IArticleCategoryService articleCategoryService)
    {
        _articleCategoryService = articleCategoryService;

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Titel ist erforderlich!")
            .MaximumLength(50).WithMessage("Titel darf nicht größer {MaxLength} Zeichen sein!")
            .MustAsync(BeNotExist).WithMessage("Titel existiert bereits!");
    }

    private async Task<bool> BeNotExist(ArticleCategoryDataModel item, string value, CancellationToken _)
        => (await _articleCategoryService.ExistByTitleAsync(value)).IsSuccess is false;
}