namespace StackNucleus.DDD.Domain.ResultModels;

/// <summary>
/// Represents the result of an operation, including the outcome status and an optional message.
/// This record is commonly used to return a simple result with a status, such as from domain entity.
/// </summary>
public record OperationResult(ResultStatus ResultStatus, string? Message = null)
{
    /// <summary>
    /// Creates a successful operation result.
    /// </summary>
    /// <returns>An instance of <see cref="OperationResult"/> with a <see cref="ResultStatus.Success"/> status.</returns>
    public static OperationResult CreateSuccess() => new(ResultStatus.Success);

    /// <summary>
    /// Creates an operation result representing a client-side error.
    /// </summary>
    /// <param name="message">The message explaining the client-side error.</param>
    /// <returns>An instance of <see cref="OperationResult"/> with a <see cref="ResultStatus.ClientSideError"/> status.</returns>
    public static OperationResult CreateClientSideError(string message) => Error(ResultStatus.ClientSideError, message);

    /// <summary>
    /// Creates an operation result representing a server-side error.
    /// </summary>
    /// <param name="message">The message explaining the server-side error.</param>
    /// <returns>An instance of <see cref="OperationResult"/> with a <see cref="ResultStatus.InternalServerError"/> status.</returns>
    public static OperationResult CreateServerSideError(string message) => Error(ResultStatus.InternalServerError, message);

    /// <summary>
    /// Creates an operation result representing a "Not Found" error.
    /// </summary>
    /// <param name="message">The message explaining the "Not Found" error.</param>
    /// <returns>An instance of <see cref="OperationResult"/> with a <see cref="ResultStatus.NotFound"/> status.</returns>
    public static OperationResult CreateNotFound(string message) => Error(ResultStatus.NotFound, message);

    /// <summary>
    /// Creates an operation result representing a validation error.
    /// </summary>
    /// <param name="message">The message explaining the validation error.</param>
    /// <returns>An instance of <see cref="OperationResult"/> with a <see cref="ResultStatus.ValidationError"/> status.</returns>
    public static OperationResult CreateValidationsError(string message) => Error(ResultStatus.ValidationError, message);

    /// <summary>
    /// Creates an operation result representing an unauthorized error.
    /// </summary>
    /// <param name="message">The message explaining the unauthorized error.</param>
    /// <returns>An instance of <see cref="OperationResult"/> with a <see cref="ResultStatus.Unauthorized"/> status.</returns>
    public static OperationResult CreateUnauthorizedError(string message) => Error(ResultStatus.Unauthorized, message);

    private static OperationResult Error(ResultStatus status, string message) => new(status, message);
}

/// <summary>
/// Represents the result of an operation that returns a value, including the outcome status, the value, 
/// and an optional message. This is a more complex version of <see cref="OperationResult"/> for operations 
/// that return data alongside a status.
/// </summary>
/// <typeparam name="T">The type of the value returned by the operation.</typeparam>
public record OperationResult<T>(ResultStatus ResultStatus, T? Value = default, string? Message = null)
    where T : class
{
    /// <summary>
    /// Creates a successful operation result with a returned value.
    /// </summary>
    /// <param name="value">The value returned by the operation.</param>
    /// <returns>An instance of <see cref="OperationResult{T}"/> with a <see cref="ResultStatus.Success"/> status and the given value.</returns>
    public static OperationResult<T> CreateSuccess(T value) => new(ResultStatus.Success, value);

    /// <summary>
    /// Creates an operation result representing a client-side error.
    /// </summary>
    /// <param name="message">The message explaining the client-side error.</param>
    /// <returns>An instance of <see cref="OperationResult{T}"/> with a <see cref="ResultStatus.ClientSideError"/> status.</returns>
    public static OperationResult<T> CreateClientSideError(string message) => Error(ResultStatus.ClientSideError, message);

    /// <summary>
    /// Creates an operation result representing a server-side error.
    /// </summary>
    /// <param name="message">The message explaining the server-side error.</param>
    /// <returns>An instance of <see cref="OperationResult{T}"/> with a <see cref="ResultStatus.InternalServerError"/> status.</returns>
    public static OperationResult<T> CreateServerSideError(string message) => Error(ResultStatus.InternalServerError, message);

    /// <summary>
    /// Creates an operation result representing a "Not Found" error.
    /// </summary>
    /// <param name="message">The message explaining the "Not Found" error.</param>
    /// <returns>An instance of <see cref="OperationResult{T}"/> with a <see cref="ResultStatus.NotFound"/> status.</returns>
    public static OperationResult<T> CreateNotFound(string message) => Error(ResultStatus.NotFound, message);

    /// <summary>
    /// Creates an operation result representing a validation error.
    /// </summary>
    /// <param name="message">The message explaining the validation error.</param>
    /// <returns>An instance of <see cref="OperationResult{T}"/> with a <see cref="ResultStatus.ValidationError"/> status.</returns>
    public static OperationResult<T> CreateValidationsError(string message) => Error(ResultStatus.ValidationError, message);

    /// <summary>
    /// Creates an operation result representing an unauthorized error.
    /// </summary>
    /// <param name="message">The message explaining the unauthorized error.</param>
    /// <returns>An instance of <see cref="OperationResult{T}"/> with a <see cref="ResultStatus.Unauthorized"/> status.</returns>
    public static OperationResult<T> CreateUnauthorizedError(string message) => Error(ResultStatus.Unauthorized, message);

    private static OperationResult<T> Error(ResultStatus status, string message) => new(status, default, message);
}
