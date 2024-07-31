using DeVesen.Bazaar.Server.Basics;
using DeVesen.Bazaar.Server.Domain;
using DeVesen.Bazaar.Shared.Basics;
using FluentValidation;

namespace DeVesen.Bazaar.Server.Validator;

public class VendorValidator : BaseValidator<Vendor>
{
    public VendorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ResourceText.Global.IdMayNotBeEmpty);

        RuleFor(x => x.Salutation)
            .NotEmpty().WithMessage(ResourceText.Global.SalutationMayNotBeEmpty)
            .MaximumLength(20).WithMessage(ResourceText.Transform(ResourceText.Global.SalutationMayNotBeLongerThan, _ => 20));

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(ResourceText.Global.FirstNameMayNotBeEmpty)
            .MaximumLength(20).WithMessage(ResourceText.Transform(ResourceText.Global.FirstNameMayNotBeLongerThan, _ => 20));

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(ResourceText.Global.LastNameMayNotBeEmpty)
            .MaximumLength(20).WithMessage(ResourceText.Transform(ResourceText.Global.LastNameMayNotBeLongerThan, _ => 20));

        RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage(ResourceText.Transform(ResourceText.Global.AddressMayNotBeLongerThan, _ => 200));

        RuleFor(x => x.EMail)
            .MaximumLength(100).WithMessage(ResourceText.Transform(ResourceText.Global.EMailMayNotBeLongerThan, _ => 100));

        RuleFor(x => x.Phone)
            .MaximumLength(50).WithMessage(ResourceText.Transform(ResourceText.Global.PhoneMayNotBeLongerThan, _ => 50));
    }
}