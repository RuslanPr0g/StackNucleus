namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents an aggregate root in Domain-Driven Design (DDD). Aggregate roots are entities that serve
/// as entry points for accessing and modifying an aggregate's internal state. They are responsible for 
/// maintaining consistency within the aggregate and exposing domain events that are part of the aggregate's lifecycle.
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    /// Gets the collection of domain events associated with this aggregate root.
    /// Domain events represent meaningful business occurrences that have happened within the aggregate.
    /// These events are typically used to notify other parts of the system or to persist state changes.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears all domain events from the aggregate root.
    /// This method is typically called after the events have been handled or persisted to prevent further processing.
    /// </summary>
    void ClearEvents();
}
