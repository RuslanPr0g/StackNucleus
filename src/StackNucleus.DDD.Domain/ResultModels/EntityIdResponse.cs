namespace StackNucleus.DDD.Domain.ResultModels;

/// <summary>
/// Represents a response containing an entity's unique identifier.
/// </summary>
public record EntityIdResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public Guid Id { get; set; }
}
