using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared.Basics;
using FluentValidation;

namespace DeVesen.Bazaar.Server.Validator;

public class ArticleValidator : BaseValidator<Article>
{
    private readonly ArticleStorage _articleStorage;
    private readonly VendorStorage _vendorStorage;

    public ArticleValidator(ArticleStorage articleStorage, VendorStorage vendorStorage)
    {
        _articleStorage = articleStorage;
        _vendorStorage = vendorStorage;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ResourceText.Global.IdMayNotBeEmpty);

        RuleFor(x => x.VendorId)
            .NotEmpty().WithMessage(ResourceText.Global.VendorIdMayNotBeEmpty)
            .MustAsync(BeVendorIdExistAsync).WithMessage(ResourceText.Global.VendorIdMayNotExist);

        RuleFor(x => x.Number)
            .GreaterThan(0).WithMessage(ResourceText.Global.NumberMayNotBeEmpty)
            .MustAsync(BeUniqueNumberAsync).WithMessage(ResourceText.Global.NumberAlreadyTaken);

        RuleFor(x => x.Description)
            .NotEmpty()
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage(ResourceText.Global.TitleMayNotBeEmpty)
            .MaximumLength(200)
                .WithMessage(ResourceText.Transform(ResourceText.Global.TitleMayNotBeLongerThan, _ => 200));

        RuleFor(x => x.ArticleCategory)
            .NotEmpty()
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage(ResourceText.Global.ArticleCategoryMayNotBeEmpty)
            .MaximumLength(100)
                .WithMessage(ResourceText.Transform(ResourceText.Global.ArticleCategoryMayNotBeLongerThan, _ => 100));

        RuleFor(x => x.Manufacturer)
            .NotEmpty()
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage(ResourceText.Global.ManufacturerMayNotBeEmpty)
            .MaximumLength(100)
                .WithMessage(ResourceText.Transform(ResourceText.Global.ManufacturerMayNotBeLongerThan, _ => 100));

        RuleFor(x => x.Price01)
            .GreaterThanOrEqualTo(0)
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage(ResourceText.Global.Price01MayNotNotBeEmpty);

        RuleFor(x => x.Price02)
            .Must(BeNullOrGreaterThan)
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage(ResourceText.Global.Price02MayNotInvalid);
    }

    private static bool BeNullOrGreaterThan(Article validateElement, double? price)
    {
        if (price.HasValue is false)
        {
            return true;
        }

        return price > 0;
    }

    private async Task<bool> BeVendorIdExistAsync(Article validateElement, string vendorId, CancellationToken _)
    {
        return await _vendorStorage.ExistByIdAsync(vendorId);
    }

    private async Task<bool> BeUniqueNumberAsync(Article validateElement, long number, CancellationToken _)
    {
        if (await _articleStorage.ExistByNumberAsync(number) is false)
        {
            return true;
        }

        var element = await _articleStorage.GetByNumberAsync(number);

        return element.Id == validateElement.Id;
    }
}