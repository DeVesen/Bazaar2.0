using FluentValidation;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Validator;

public class ArticleDescriptionValidator : BaseValidator<ArticleDescription>
{
    private readonly IArticleBrandService _articleBrandService;
    private readonly IArticleCategoryService _articleCategoryService;

    public ArticleDescriptionValidator(IArticleBrandService articleBrandService, IArticleCategoryService articleCategoryService)
    {
        _articleBrandService = articleBrandService;
        _articleCategoryService = articleCategoryService;

        RuleFor(item => item.Description)
            .NotEmpty().WithMessage("Beschreibung ist erforderlich!")
            .MaximumLength(200).WithMessage("Beschreibung darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(item => item.ArticleCategory)
            .NotEmpty().WithMessage("Kategorie ist erforderlich!")
            .MustAsync(BeExistAsArticleCategoryAsync).WithMessage("Kategorie ist nicht bekannt!");

        RuleFor(item => item.ArticleBrand)
            .NotEmpty().WithMessage("Hersteller ist erforderlich!")
            .MustAsync(BeExistAsArticleBrandAsync).WithMessage("Hersteller ist nicht bekannt!");
    }

    private async Task<bool> BeExistAsArticleCategoryAsync(ArticleDescription item, string value, CancellationToken _)
        => (await _articleCategoryService.ExistByTitleAsync(value)).IsSuccess;

    private async Task<bool> BeExistAsArticleBrandAsync(ArticleDescription item, string value, CancellationToken _)
        => (await _articleBrandService.ExistByTitleAsync(value)).IsSuccess;
}