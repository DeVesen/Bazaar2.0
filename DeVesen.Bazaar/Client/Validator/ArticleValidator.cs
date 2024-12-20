﻿using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Client.Services;
using DeVesen.Bazaar.Shared.Basics;
using FluentValidation;

namespace DeVesen.Bazaar.Client.Validator;

public class ArticleValidator : BaseValidator<Article>
{
    private readonly ArticleCategoryService _articleCategoryService;
    private readonly ManufacturerService _manufacturerService;

    public ArticleValidator(ArticleCategoryService articleCategoryService, ManufacturerService manufacturerService)
    {
        _articleCategoryService = articleCategoryService;
        _manufacturerService = manufacturerService;

        RuleFor(x => x.Number)
            .Must(x => x > 0).WithMessage("Muss größer 0 sein!");

        RuleFor(x => x.Description)
            .NotEmpty()
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage("Darf nicht leer sein!")
            .MaximumLength(200)
                .WithMessage("Darf nicht lönger 200 Zeichen sein!");

        RuleFor(x => x.ArticleCategory)
            .NotEmpty().WithMessage("Darf nicht leer sein!")
            .MustAsync(BeExistAsArticleCategoryAsync)
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage("Ist nicht bekannt!").WithErrorCode("NotKnown");

        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessage("Darf nicht leer sein!")
            .MustAsync(BeExistAsManufacturerAsync)
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage("Ist nicht bekannt!").WithErrorCode("NotKnown");

        RuleFor(x => x.Price01)
            .Must(x => x > 0)
                .When(p => p.Manufacturer.Equals("00 Mitgebracht", StringComparison.OrdinalIgnoreCase) is false)
                    .WithMessage("Muss größer 0 sein!");
    }

    private async Task<bool> BeExistAsArticleCategoryAsync(Article dto, string value, CancellationToken _)
    {
        return await _articleCategoryService.ExistsAsync(value) is { IsValid: true, Value: true };
    }

    private async Task<bool> BeExistAsManufacturerAsync(Article dto, string value, CancellationToken _)
    {
        return await _manufacturerService.ExistsAsync(value) is { IsValid: true, Value: true };
    }

    public new Func<object, string, Task<IEnumerable<string>>> ValidateAsync =>
        async (model, propertyName) =>
        {
            var result = (await base.ValidateAsync((Article)model, propertyName)).ToArray();

            return result;
        };
}