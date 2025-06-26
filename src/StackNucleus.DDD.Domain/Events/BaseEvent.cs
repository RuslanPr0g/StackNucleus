namespace StackNucleus.DDD.Domain.Events;

/// <summary>
/// Represents the base class for all events in the system. Events can be either domain events or integration events.
/// This class contains common properties such as <see cref="CorrelationId"/> and <see cref="Version"/>.
/// </summary>
public abstract class BaseEvent
{
    /// <summary>
    /// Gets or sets the correlation ID for the event. This is used to track and correlate events across different systems or actions.
    /// </summary>
    public Guid CorrelationId { get; set; }

    /// <summary>
    /// Gets the version of the event. This property is used to track changes to event schemas and ensures versioning.
    /// </summary>
    public int Version { get; init; } = 1;
}