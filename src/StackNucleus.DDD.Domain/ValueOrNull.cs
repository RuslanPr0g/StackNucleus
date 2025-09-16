namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents either a successful value or a failure with an error message.
/// </summary>
/// <typeparam name="T">The type of value being wrapped.</typeparam>
public sealed record ValueOrNull<T>
{
    /// <summary>
    /// Indicates whether the operation succeeded and a value is available.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// The value returned by the operation, if successful.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Whether the value is not null.
    /// </summary>
    public bool HasValue => Value is not null;

    /// <summary>
    /// The error message explaining the failure, if not successful.
    /// </summary>
    public string? Error { get; }

    private ValueOrNull(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private ValueOrNull(string error)
    {
        IsSuccess = false;
        Error = error;
    }

    /// <summary>
    /// Creates a successful result with a value.
    /// </summary>
    public static ValueOrNull<T> Success(T value) => new(value);

    /// <summary>
    /// Creates a failed result with an error message.
    /// </summary>
    public static ValueOrNull<T> Failure(string error) => new(error);
}
