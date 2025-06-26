using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackNucleus.DDD.Domain.ResultModels;

namespace StackNucleus.DDD.Api.Rest;

/// <summary>
/// Extension methods for converting <see cref="OperationResult"/> and <see cref="OperationResult{T}"/>
/// to HTTP result types like <see cref="IResult"/> for returning appropriate responses in web APIs.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts an <see cref="OperationResult"/> to an <see cref="IResult"/> for HTTP response.
    /// The result status determines the corresponding HTTP status code and response message.
    /// </summary>
    /// <param name="result">
    /// The <see cref="OperationResult"/> to convert.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the corresponding HTTP response.
    /// </returns>
    public static IResult OperationToHttpResult(this OperationResult? result)
    {
        if (result is null)
        {
            return Results.NotFound("Operation could not be completed as the required resource returned an empty result.");
        }

        return result.ResultStatus switch
        {
            ResultStatus.Success => Results.Ok(),
            ResultStatus.NotFound => Results.NotFound(new { result.Message }),
            ResultStatus.Unauthorized => Results.Unauthorized(),
            ResultStatus.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden),
            ResultStatus.ValidationError => Results.BadRequest(new { result.Message }),
            ResultStatus.ClientSideError => Results.BadRequest(new { result.Message }),
            ResultStatus.InternalServerError => Results.StatusCode(StatusCodes.Status500InternalServerError),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    /// <summary>
    /// Converts an <see cref="OperationResult{T}"/> to an <see cref="IResult"/> for HTTP response.
    /// The result status determines the corresponding HTTP status code and response message,
    /// with the value returned in the response body if the operation is successful.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value returned by the <see cref="OperationResult{T}"/>.
    /// </typeparam>
    /// <param name="result">
    /// The <see cref="OperationResult{T}"/> to convert.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the corresponding HTTP response.
    /// </returns>
    public static IResult OperationToHttpResult<T>(this OperationResult<T>? result) where T : class
    {
        if (result is null)
        {
            return Results.NotFound("Operation could not be completed as the required resource returned an empty result.");
        }

        return result.ResultStatus switch
        {
            ResultStatus.Success => Results.Ok(result.Value),
            ResultStatus.NotFound => Results.NotFound(new { result.Message }),
            ResultStatus.Unauthorized => Results.Unauthorized(),
            ResultStatus.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden),
            ResultStatus.ValidationError => Results.BadRequest(new { result.Message }),
            ResultStatus.ClientSideError => Results.BadRequest(new { result.Message }),
            ResultStatus.InternalServerError => Results.StatusCode(StatusCodes.Status500InternalServerError),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    /// <summary>
    /// Converts a regular result of type <typeparamref name="T"/> to an <see cref="IResult"/> for HTTP response.
    /// If the result is <c>null</c>, a <see cref="NotFoundResult"/> is returned; otherwise, an <see cref="OkResult"/> is returned.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the result returned.
    /// </typeparam>
    /// <param name="result">
    /// The result to convert.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the corresponding HTTP response.
    /// </returns>
    public static IResult ToHttpResult<T>(this T? result) where T : class?
    {
        return result is null ? Results.NotFound() : Results.Ok(result);
    }
}