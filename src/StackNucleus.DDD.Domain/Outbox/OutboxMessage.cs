namespace StackNucleus.DDD.Domain.Outbox;

/// <summary>
/// Represents an outbox message used for ensuring reliable communication across systems or services.
/// This message is typically used in event-driven architectures to store events or messages 
/// before they are published to external systems or message brokers.
/// </summary>
public class OutboxMessage
{
    /// <summary>
    /// Gets or sets the unique identifier of the outbox message.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the outbox message. 
    /// This could be used to differentiate between different kinds of messages or events.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the outbox message. This is typically the serialized data of the event or message.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the message occurred in UTC.
    /// This is typically the time when the event that generated the message happened.
    /// </summary>
    public DateTime OccuredOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the message was processed in UTC.
    /// This value will be <c>null</c> if the message has not yet been processed.
    /// </summary>
    public DateTime? ProcessedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the error message (if any) associated with the processing of the outbox message.
    /// This is typically used to log errors that occurred during message processing.
    /// </summary>
    public string? Error { get; set; }
}
