namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents a domain event that signifies something important has occurred
/// within the domain model. Domain events are typically used to trigger side effects
/// or notify other parts of the system.
/// </summary>
public interface IDomainEvent : IBaseEvent
{
}
