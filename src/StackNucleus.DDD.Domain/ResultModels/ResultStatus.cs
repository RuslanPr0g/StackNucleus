namespace StackNucleus.DDD.Domain.ResultModels;

/// <summary>
/// Represents the various statuses that a result can have in a response context.
/// This enum is commonly used to indicate the outcome of an operation or API call.
/// </summary>
public enum ResultStatus
{
    /// <summary>
    /// The operation was successful.
    /// </summary>
    Success,

    /// <summary>
    /// The requested resource was not found.
    /// </summary>
    NotFound,

    /// <summary>
    /// The user is not authorized to perform the operation.
    /// </summary>
    Unauthorized,

    /// <summary>
    /// The user is forbidden from accessing the requested resource.
    /// </summary>
    Forbidden,

    /// <summary>
    /// The operation encountered a validation error.
    /// </summary>
    ValidationError,

    /// <summary>
    /// The error occurred on the client-side, such as a malformed request.
    /// </summary>
    ClientSideError,

    /// <summary>
    /// An unexpected error occurred on the server-side.
    /// </summary>
    InternalServerError
}
