using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Client.Services;
using DeVesen.Bazaar.Shared.Basics;
using FluentValidation;

namespace DeVesen.Bazaar.Client.Validator;

public class ArticleCategoryValidator : BaseValidator<ArticleCategory>
{

    public ArticleCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("'Id' darf nicht leer sein!");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("'Bezeichnung' darf nicht leer sein!")
            .NotEmpty().WithMessage("'Bezeichnung' darf nicht leer sein!")
            .MaximumLength(20).WithMessage("'Bezeichnung' darf nicht lönger 20 Zeichen sein!")
            .MustAsync(BeUniqueNameAsync).WithMessage("Name ist bereits vergeben!");
    }

    private async Task<bool> BeUniqueNameAsync(ArticleCategory dto, string name, CancellationToken _)
    {
        return await Task.FromResult(true);
    }

    public new Func<object, string, Task<IEnumerable<string>>> ValidateAsync =>
        async (model, propertyName) =>
            await base.ValidateAsync((ArticleCategory)model, propertyName);
}