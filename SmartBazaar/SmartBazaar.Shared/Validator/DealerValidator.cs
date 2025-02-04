using FluentValidation;
using SmartBazaar.Shared.Extensions;
using SmartBazaar.Shared.Models;

namespace SmartBazaar.Shared.Validator;

public class DealerValidator : BaseValidator<DealerDataModel>
{
    public DealerValidator()
    {
        RuleFor(x => x.Name.FirstName)
            .NotEmpty().When(p => p.IsCompany() is false).WithMessage("Vorname ist erforderlich!")
            .MaximumLength(30).When(p => p.IsCompany() is false).WithMessage("Vorname darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.Name.LastName)
            .NotEmpty().WithMessage("Nachname ist erforderlich!")
            .MaximumLength(50).WithMessage("Nachname darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.Address.Street)
            .MaximumLength(100).WithMessage("Straße darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.Address.ZipCode)
            .MaximumLength(10).WithMessage("Postleitzahl darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.Address.City)
            .NotEmpty().WithMessage("Stadt/Ort ist erforderlich!")
            .MaximumLength(200).WithMessage("Anschrift darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.Contact.EMail)
            .MaximumLength(75).WithMessage("E-Mail darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.Contact.Phone)
            .MaximumLength(75).WithMessage("Telefon darf nicht größer {MaxLength} Zeichen sein!");

        RuleFor(x => x.SalesShareRatio.SalesShare)
            .GreaterThanOrEqualTo(0).WithMessage("Umsatzanteil darf nicht kleiner {ComparisonValue} sein!");

        RuleFor(x => x.SalesShareRatio.OfferUnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Stückpreis darf nicht kleiner {ComparisonValue} sein!");

        RuleFor(x => x.Note)
            .MaximumLength(10).WithMessage("Notiz darf nicht lönger {MaxLength} Zeichen sein!");
    }
}