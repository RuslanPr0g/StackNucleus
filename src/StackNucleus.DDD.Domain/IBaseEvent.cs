namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents the base event interface that all events in the system should implement.
/// It includes the correlation ID for tracking and a version for event evolution.
/// </summary>
public interface IBaseEvent
{
    /// <summary>
    /// Gets or sets the correlation ID that can be used to correlate events
    /// across different systems or processes.
    /// </summary>
    Guid CorrelationId { get; set; }

    /// <summary>
    /// Gets the version of the event. This is useful for event versioning
    /// and handling changes in event structure over time.
    /// </summary>
    int Version { get; init; }
}
