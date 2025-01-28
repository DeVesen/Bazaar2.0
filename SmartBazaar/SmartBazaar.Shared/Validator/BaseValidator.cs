using FluentValidation;

namespace SmartBazaar.Shared.Validator;

public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
{
    public new Func<object, string, Task<IEnumerable<string>>> Validate =>
        async (model, propertyName) =>
            await ValidateAsync((T)model, propertyName);

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