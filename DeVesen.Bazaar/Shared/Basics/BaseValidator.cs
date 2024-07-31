using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared.Basics;

[ExcludeFromCodeCoverage]
public abstract class BaseValidator<T> : AbstractValidator<T>
{
    protected async Task<IEnumerable<string>> ValidateAsync(T value, string propertyName, params string[] ruleSet)
    {
        var validationContext = ValidationContext<T>.CreateWithOptions(value, validationStrategy =>
        {
            validationStrategy.IncludeProperties(propertyName);
            validationStrategy.IncludeRuleSets(ruleSet);
        });
        var result = await ValidateAsync(validationContext);

        return result.Errors.Select(error => error.ErrorMessage);
    }
}