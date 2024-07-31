using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Server.Storage;
using DeVesen.Bazaar.Shared.Basics;
using FluentValidation;

namespace DeVesen.Bazaar.Server.Validator;

public class ManufacturerValidator : BaseValidator<Manufacturer>
{
    private readonly ManufacturerStorage _manufacturerStorage;

    public ManufacturerValidator(ManufacturerStorage manufacturerStorage)
    {
        _manufacturerStorage = manufacturerStorage;

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ResourceText.Global.IdMayNotBeEmpty);

        RuleFor(x => x.Name)
            .NotNull().WithMessage(ResourceText.Global.NameMayNotBeEmpty)
            .NotEmpty().WithMessage(ResourceText.Global.NameMayNotBeEmpty)
            .MaximumLength(20).WithMessage(ResourceText.Transform(ResourceText.Global.NameMayNotBeLongerThan, _ => 20))
            .MustAsync(BeUniqueNameAsync).WithMessage(ResourceText.Global.NameAlreadyTaken);
    }

    private async Task<bool> BeUniqueNameAsync(Manufacturer validateElement, string name, CancellationToken _)
    {
        if (await _manufacturerStorage.ExistByNameAsync(name) is false)
        {
            return true;
        }

        var element = await _manufacturerStorage.GetByNameAsync(name);

        return element.Id == validateElement.Id;
    }
}