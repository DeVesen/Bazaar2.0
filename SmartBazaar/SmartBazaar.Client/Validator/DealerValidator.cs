using FluentValidation;
using SmartBazaar.Client.Models;
using SmartBazaar.Client.Services;
using SmartBazaar.Shared.Validator;

namespace SmartBazaar.Client.Validator;

public class DealerValidator : BaseValidator<Dealer>
{
    public DealerValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Vorname ist erforderlich!")
            .MaximumLength(30).WithMessage("Vorname darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Nachname ist erforderlich!")
            .MaximumLength(50).WithMessage("Nachname darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Anschrift ist erforderlich!")
            .MaximumLength(200).WithMessage("Anschrift darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.EMail)
            .MaximumLength(75).WithMessage("E-Mail darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.Phone)
            .MaximumLength(75).WithMessage("Telefon darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.SalesShare)
            .GreaterThan(0).WithMessage("Umsatzanteil darf nicht kleiner {ComparisonValue} sein!");

        RuleFor(x => x.OfferUnitPrice)
            .GreaterThan(0).WithMessage("Stückpreis darf nicht kleiner {ComparisonValue} sein!");

        RuleFor(x => x.Note)
            .MaximumLength(10).WithMessage("Notiz darf nicht lönger {MaxLength} Zeichen sein!");
    }
}

public class ArticleBrandValidator : BaseValidator<ArticleBrand>
{
    private readonly IArticleBrandService _articleBrandService;

    public ArticleBrandValidator(IArticleBrandService articleBrandService)
    {
        _articleBrandService = articleBrandService;

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Titel ist erforderlich!")
            .MaximumLength(50).WithMessage("Titel darf nicht größer {MaxLength} Zeichen sein!")
            .MustAsync(BeNotExist).WithMessage("Titel existiert bereits!");
    }

    private async Task<bool> BeNotExist(ArticleBrand item, string value, CancellationToken _)
        => (await _articleBrandService.ExistByTitleAsync(value)).IsSuccess is false;
}

public class ArticleCategoryValidator : BaseValidator<ArticleCategory>
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

    private async Task<bool> BeNotExist(ArticleCategory item, string value, CancellationToken _)
        => (await _articleCategoryService.ExistByTitleAsync(value)).IsSuccess is false;
}


public class ArticleDescriptionValidator : BaseValidator<ArticleDescription>
{
    private readonly IArticleBrandService _articleBrandService;
    private readonly IArticleCategoryService _articleCategoryService;

    public ArticleDescriptionValidator(IArticleBrandService articleBrandService, IArticleCategoryService articleCategoryService)
    {
        _articleBrandService = articleBrandService;
        _articleCategoryService = articleCategoryService;

        RuleFor(item => item.Number)
            .NotEmpty().WithMessage("Nummer ist erforderlich!")
            .MaximumLength(10).WithMessage("Titel darf nicht größer {MaxLength} Zeichen sein!");

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

public class ArticlePriceValidator : BaseValidator<ArticlePrice>
{
    public ArticlePriceValidator()
    {
        RuleFor(item => item.Price01)
            .NotEmpty()
                .WithMessage("Preis ist erforderlich (> 0)!")
            .Must(x => x > 0)
                .WithMessage("Preis ist erforderlich (> 0)!");

        RuleFor(item => item.Price02)
            .Must(x => x.HasValue is false || x > 0)
                .WithMessage("Preis alternative > 0 oder leer!");
    }
}