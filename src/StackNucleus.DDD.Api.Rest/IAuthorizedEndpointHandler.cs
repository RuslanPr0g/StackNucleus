using Microsoft.AspNetCore.Http;
using StackNucleus.DDD.Domain.ResultModels;

namespace StackNucleus.DDD.Api.Rest;

/// <summary>
/// Represents a handler for endpoints that require user authorization. This interface provides methods
/// for executing actions that require a <see cref="CurrentUser"/> and return results based on user operations.
/// </summary>
public interface IAuthorizedEndpointHandler
{
    /// <summary>
    /// Executes the specified action that requires the current user and returns a result asynchronously.
    /// </summary>
    /// <typeparam name="TActionResult">
    /// The type of the result to be returned by the action. Must be a reference type.
    /// </typeparam>
    /// <param name="action">
    /// A function that takes the current user and returns the result asynchronously.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the result of the action execution.
    /// </returns>
    Task<IResult> WithUser<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : class?;

    /// <summary>
    /// Executes the specified action that requires the current user and returns a result synchronously.
    /// </summary>
    /// <typeparam name="TActionResult">
    /// The type of the result to be returned by the action. Must be a reference type.
    /// </typeparam>
    /// <param name="action">
    /// A function that takes the current user and returns the result.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the result of the action execution.
    /// </returns>
    IResult WithUser<TActionResult>(Func<CurrentUser, TActionResult> action)
        where TActionResult : class?;

    /// <summary>
    /// Executes the specified action that requires the current user and returns an operation result asynchronously.
    /// </summary>
    /// <typeparam name="TActionResult">
    /// The type of the operation result to be returned by the action.
    /// </typeparam>
    /// <param name="action">
    /// A function that takes the current user and returns the operation result asynchronously.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the result of the operation execution.
    /// </returns>
    Task<IResult> WithUserOperation<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : OperationResult?;

    /// <summary>
    /// Executes the specified action that requires the current user and returns an operation result with a value asynchronously.
    /// </summary>
    /// <typeparam name="TActionResultValue">
    /// The type of the value to be returned in the operation result.
    /// </typeparam>
    /// <param name="action">
    /// A function that takes the current user and returns the operation result with a value asynchronously.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the result of the operation execution with a value.
    /// </returns>
    Task<IResult> WithUserOperationValue<TActionResultValue>(
        Func<CurrentUser, Task<OperationResult<TActionResultValue>>> action)
        where TActionResultValue : class;
}