namespace StackNucleus.DDD.Domain.ResultModels;

/// <summary>
/// Represents a collection of error messages.
/// </summary>
public record ErrorResult(List<ErrorMessage> Errors);
