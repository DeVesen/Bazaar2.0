using FluentValidation;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Validator;

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