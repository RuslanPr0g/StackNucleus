namespace StackNucleus.DDD.Domain.EventHandlers;

/// <summary>
/// Represents a handler for domain events. This interface extends the base <see cref="IEventHandler{TMessage}"/>
/// interface and is specifically for handling <see cref="IDomainEvent"/> messages.
/// </summary>
/// <typeparam name="TMessage">
/// The type of the domain event to be handled, which must implement <see cref="IBaseEvent"/>.
/// </typeparam>
public interface IDomainEventHandler<TMessage> : IEventHandler<TMessage>
    where TMessage : IBaseEvent
{
}
