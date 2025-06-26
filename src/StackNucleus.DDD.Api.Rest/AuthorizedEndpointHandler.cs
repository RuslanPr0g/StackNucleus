﻿using Microsoft.AspNetCore.Http;
using StackNucleus.DDD.Domain.ResultModels;

namespace StackNucleus.DDD.Api.Rest;

/// <inheritdoc cref="IAuthorizedEndpointHandler" />
public class AuthorizedEndpointHandler(IHttpContextAccessor contextAccessor) : IAuthorizedEndpointHandler
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    /// <inheritdoc cref="IAuthorizedEndpointHandler.WithUser{TActionResult}(Func{CurrentUser, Task{TActionResult}})" />
    public async Task<IResult> WithUser<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : class?
    {
        return await HandleUserContextAsync(
            _contextAccessor.HttpContext,
            action,
            result => result.ToHttpResult());
    }

    /// <inheritdoc cref="IAuthorizedEndpointHandler.WithUser{TActionResult}(Func{CurrentUser, TActionResult})" />
    public IResult WithUser<TActionResult>(Func<CurrentUser, TActionResult> action)
        where TActionResult : class?
    {
        return HandleUserContext(
            _contextAccessor.HttpContext,
            action,
            result => result.ToHttpResult());
    }

    /// <inheritdoc cref="IAuthorizedEndpointHandler.WithUserOperation{TActionResult}(Func{CurrentUser, Task{TActionResult}})" />
    public async Task<IResult> WithUserOperation<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : OperationResult?
    {
        return await HandleUserContextAsync(
            _contextAccessor.HttpContext,
            action,
            result => result.OperationToHttpResult());
    }

    /// <inheritdoc cref="IAuthorizedEndpointHandler.WithUserOperationValue{TActionResultValue}(Func{CurrentUser, Task{OperationResult{TActionResultValue}}})" />
    public async Task<IResult> WithUserOperationValue<TActionResultValue>(
        Func<CurrentUser, Task<OperationResult<TActionResultValue>>> action)
        where TActionResultValue : class
    {
        return await HandleUserContextAsync(
            _contextAccessor.HttpContext,
            action,
            result => result.OperationToHttpResult());
    }

    private static IResult HandleUserContext<T>(
        HttpContext? context,
        Func<CurrentUser, T> action,
        Func<T, IResult> resultConverter)
    {
        if (context is null)
        {
            return Results.BadRequest("Something went wrong when processing the http request.");
        }

        var user = CurrentUser.FromClaims(context.User);
        if (user is null)
        {
            return Results.BadRequest("Unable to interpret the provided token.");
        }

        var result = action(user);
        return resultConverter(result);
    }

    private static async Task<IResult> HandleUserContextAsync<T>(
        HttpContext? context,
        Func<CurrentUser, Task<T>> action,
        Func<T, IResult> resultConverter)
    {
        if (context is null)
        {
            return Results.BadRequest("Something went wrong when processing the http request.");
        }

        var user = CurrentUser.FromClaims(context.User);
        if (user is null)
        {
            return Results.BadRequest("Unable to interpret the provided token.");
        }

        var result = await action(user);
        return resultConverter(result);
    }
}
