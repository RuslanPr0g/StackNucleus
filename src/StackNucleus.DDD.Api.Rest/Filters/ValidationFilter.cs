using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StackNucleus.DDD.Domain.ResultModels;

namespace StackNucleus.DDD.Api.Rest.Filters;

/// <summary>
/// A filter that validates the model state of an action and returns a BadRequest response if invalid.
/// </summary>
public class ValidationFilter : IAsyncActionFilter
{
    /// <summary>
    /// Executes the validation logic before the action is executed.
    /// If the model state is invalid, returns a BadRequest response with the validation errors.
    /// </summary>
    /// <param name="context">The context for the action execution.</param>
    /// <param name="next">The delegate to the next filter or action.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            KeyValuePair<string, IEnumerable<string>>[] errors = context.ModelState
                .Where(x => x.Value is not null && x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value is null ? [] : kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            ErrorResult errorResponse = new(new List<ErrorMessage>());

            foreach (KeyValuePair<string, IEnumerable<string>> error in errors)
                foreach (string subError in error.Value)
                {
                    ErrorMessage errorModel = new()
                    {
                        FieldName = error.Key,
                        Message = subError
                    };

                    errorResponse.Errors.Add(errorModel);
                }

            context.Result = new BadRequestObjectResult(errorResponse);
            return;
        }

        await next();
    }
}