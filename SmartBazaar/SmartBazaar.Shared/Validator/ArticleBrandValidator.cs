using FluentValidation;
using SmartBazaar.Shared.Contracts;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Validator;

public class ArticleBrandValidator : BaseValidator<ArticleBrandDataModel>
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

    private async Task<bool> BeNotExist(ArticleBrandDataModel item, string value, CancellationToken _)
        => (await _articleBrandService.ExistByTitleAsync(value)).IsSuccess is false;
}