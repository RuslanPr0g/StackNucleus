namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents an auditable entity that tracks its creation, modification times,
/// and version for auditing and version control purposes.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last edited.
    /// </summary>
    DateTime EditedAt { get; set; }

    /// <summary>
    /// Gets or sets the version number of the entity. This can be used to track
    /// changes to the entity over time.
    /// </summary>
    int Version { get; set; }
}
