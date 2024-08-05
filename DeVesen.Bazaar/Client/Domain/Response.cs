namespace DeVesen.Bazaar.Client.Domain;

public record Response<T> : Response
{
    public T Value { get; init; } = default!;

    public new static Response<T> Invalid()
        => new() { IsValid = false };

    public static Response<T> Valid(T value)
        => new() { IsValid = true, Value = value };
}

public record Response
{
    public required bool IsValid { get; init; }

    public IEnumerable<string> ErrorMessages { get; init; } = Enumerable.Empty<string>();

    public static Response Invalid()
        => new() { IsValid = false };

    public static Response Valid()
        => new() { IsValid = true };
}