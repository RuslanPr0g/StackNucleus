namespace StackNucleus.DDD.Domain.ResultModels;

/// <summary>
/// Represents an error message with a field name and an associated message.
/// </summary>
public record ErrorMessage
{
    /// <summary>
    /// Gets or sets the name of the field where the error occurred.
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// Gets or sets the error message related to the field.
    /// </summary>
    public string Message { get; set; }
}
