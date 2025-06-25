namespace StackNucleus.DDD.Domain.EventHandlers;

/// <summary>
/// Defines the contract for an event handler that processes a specific type of event.
/// The <see cref="IEventHandler{TEvent}"/> interface is used by classes that handle events of type <typeparamref name="TEvent"/>.
/// </summary>
/// <typeparam name="TEvent">The type of event that this handler processes. This type must implement <see cref="IBaseEvent"/>.</typeparam>
public interface IEventHandler<TEvent> where TEvent : IBaseEvent
{
}
