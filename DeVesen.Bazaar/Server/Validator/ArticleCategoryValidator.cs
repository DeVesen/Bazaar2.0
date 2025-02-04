using DeVesen.Bazaar.Server.Infrastructure;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared.Basics;
using FluentValidation;

namespace DeVesen.Bazaar.Server.Validator;

public class ArticleCategoryValidator : BaseValidator<ArticleCategoryEntity>
{
    private readonly ArticleCategoryStorage _articleCategoryStorage;


    public ArticleCategoryValidator(bool isCreate, ArticleCategoryStorage articleCategoryStorage) : base(isCreate)
    {
        _articleCategoryStorage = articleCategoryStorage;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("'Id' darf nicht leer sein!")
            .MustAsync(IdIsNotKnown).When(IsCreate).WithMessage("'Id' ist bereits bekannt!")
            .MustAsync(IdIsKnown).When(IsUpdate).WithMessage("'Id' ist nicht bekannt!");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("'Bezeichnung' darf nicht leer sein!")
            .MaximumLength(100).WithMessage("'Bezeichnung' darf nicht lönger 100 Zeichen sein!")
            .MustAsync(BeUniqueNameAsync).WithMessage("'Bezeichnung' ist bereits vergeben!");
    }


    private async Task<bool> IdIsKnown(string id, CancellationToken _)
        => await _articleCategoryStorage.ExistByIdAsync(id);

    private async Task<bool> IdIsNotKnown(string id, CancellationToken _)
        => await _articleCategoryStorage.ExistByIdAsync(id) is false;

    private async Task<bool> BeUniqueNameAsync(ArticleCategoryEntity validateElement, string name, CancellationToken _)
    {
        if (await _articleCategoryStorage.ExistByNameAsync(name) is false)
        {
            return true;
        }

        var element = await _articleCategoryStorage.GetByNameAsync(name);

        return element.Id == validateElement.Id;
    }
}