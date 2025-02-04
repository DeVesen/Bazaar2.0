namespace SmartBazaar.Shared;

public class Result : IResult
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }


    private Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }


    public static IResult Success()
        => Create(true, string.Empty);

    public static IResult Fail(string message)
        => Create(false, message);

    public static IResult Create(bool success, string errorMessage)
        => new Result(success, errorMessage);
}

public interface IResult
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
}

public class Result<T> : IResult<T>
    where T : class
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public T Value { get; }


    private Result(bool isSuccess, string errorMessage, T value)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Value = value;
    }


    public static IResult<T> Success(T value)
        => Create(true, string.Empty, value);

    public static IResult<T> Fail(string errorMessage)
        => Create(false, errorMessage, default!);

    public static IResult<T> Create(bool success, string errorMessage, T? value)
        => new Result<T>(success, errorMessage, (value ?? default)!);
}

public interface IResult<out T> : IResult
    where T : class
{
    public T Value { get; }
}