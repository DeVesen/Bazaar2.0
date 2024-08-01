using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared.Basics;
using FluentValidation;

namespace DeVesen.Bazaar.Client.Validator;

public class VendorValidator : BaseValidator<Vendor>
{
    public VendorValidator()
    {
        RuleFor(x => x.Salutation)
            .NotEmpty().WithMessage("Darf nicht leer sein!")
            .MaximumLength(20).WithMessage("Darf nicht lönger 20 Zeichen sein!");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Darf nicht leer sein!")
            .MaximumLength(20).WithMessage("Darf nicht lönger 20 Zeichen sein!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Darf nicht leer sein!")
            .MaximumLength(20).WithMessage("Darf nicht lönger 20 Zeichen sein!");

        RuleFor(x => x.LastName)
            .MaximumLength(200).WithMessage("Darf nicht lönger 200 Zeichen sein!");

        RuleFor(x => x.EMail)
            .MaximumLength(100).WithMessage("Darf nicht lönger 100 Zeichen sein!");

        RuleFor(x => x.Phone)
            .MaximumLength(50).WithMessage("Darf nicht lönger 50 Zeichen sein!");
    }

    public new Func<object, string, Task<IEnumerable<string>>> ValidateAsync =>
        async (model, propertyName) =>
            await base.ValidateAsync((Vendor)model, propertyName);
}