using StackNucleus.DDD.Domain;
using StackNucleus.DDD.Domain.EventPublishers;
using Wolverine;

namespace StackNucleus.DDD.Events.WolverineFx;

/// <summary>
/// A base implementation of an event publisher that uses an <see cref="IMessageBus"/> to publish events asynchronously.
/// </summary>
public class BaseEventPublisher : IEventPublisher
{
    private readonly IMessageBus _bus;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEventPublisher"/> class.
    /// </summary>
    /// <param name="bus">
    /// The <see cref="IMessageBus"/> used to publish events.
    /// </param>
    public BaseEventPublisher(IMessageBus bus)
    {
        _bus = bus;
    }

    /// <summary>
    /// Publishes an event asynchronously to the message bus.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the event to be published, which must implement <see cref="IBaseEvent"/>.
    /// </typeparam>
    /// <param name="event">
    /// The event to be published.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask"/> representing the asynchronous operation of publishing the event.
    /// </returns>
    public ValueTask Publish<T>(T @event) where T : IBaseEvent
    {
        return _bus.PublishAsync(@event);
    }
}