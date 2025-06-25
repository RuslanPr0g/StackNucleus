namespace StackNucleus.DDD.Domain.EventPublishers;

/// <summary>
/// Defines the contract for an event publisher that is responsible for publishing events to subscribers.
/// The <see cref="IEventPublisher"/> interface enables asynchronous event publishing for different types of events.
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// Publishes an event asynchronously to its subscribers.
    /// </summary>
    /// <typeparam name="T">The type of the event to publish. This type must implement <see cref="IBaseEvent"/>.</typeparam>
    /// <param name="event">The event to be published.</param>
    /// <returns>A task that represents the asynchronous operation of publishing the event.</returns>
    ValueTask Publish<T>(T @event) where T : IBaseEvent;
}
