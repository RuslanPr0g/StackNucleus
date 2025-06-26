namespace StackNucleus.DDD.Domain.EventHandlers;

/// <summary>
/// Represents a handler for integration events. This interface extends the base <see cref="IEventHandler{TMessage}"/>
/// interface and is specifically for handling <see cref="IIntegrationEvent"/> messages that are typically used to communicate 
/// across different systems or bounded contexts.
/// </summary>
/// <typeparam name="TMessage">
/// The type of the integration event to be handled, which must implement <see cref="IBaseEvent"/>.
/// </typeparam>
public interface IIntegrationEventHandler<TMessage> : IEventHandler<TMessage>
    where TMessage : IBaseEvent
{
}