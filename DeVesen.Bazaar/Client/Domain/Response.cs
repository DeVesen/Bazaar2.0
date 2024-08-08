namespace DeVesen.Bazaar.Client.Domain;

public record Response<T> : Response
{
    public T Value { get; init; } = default!;

    public new static Response<T> Invalid(params string[] errorMessages)
        => new() { IsValid = false, ErrorMessages = errorMessages };

    public new static Response<T> Valid()
        => Valid(default!);

    public static Response<T> Valid(T value)
        => new() { IsValid = true, Value = value };
}

public record Response
{
    public required bool IsValid { get; init; }

    public string ErrorMessage => ErrorMessages.FirstOrDefault() ?? string.Empty;

    public IEnumerable<string> ErrorMessages { get; init; } = Enumerable.Empty<string>();

    public static Response Invalid(params string[] errorMessages)
        => new() { IsValid = false, ErrorMessages = errorMessages };

    public static Response Valid()
        => new() { IsValid = true };
}