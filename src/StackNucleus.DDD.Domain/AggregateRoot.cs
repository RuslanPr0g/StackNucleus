namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents an aggregate root with a typed identifier. An aggregate root is an entity within a bounded context
/// that serves as the entry point for interacting with an aggregate. It manages its own domain events and maintains 
/// the consistency of the aggregate boundary.
/// </summary>
/// <typeparam name="T">
/// The type of the identifier, which must be a subclass of <see cref="Identity"/>.
/// </typeparam>
public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot where T : Identity
{
    private readonly List<IDomainEvent> _domainEvents;

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{T}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the aggregate root.</param>
    protected AggregateRoot(T id) : base(id)
    {
        Id = id;
        _domainEvents = new List<IDomainEvent>();
    }

    /// <summary>
    /// Gets the collection of domain events associated with this aggregate root.
    /// Domain events represent business events that are important for this aggregate.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList().AsReadOnly();

    /// <summary>
    /// Clears all domain events from the aggregate root after they have been handled or persisted.
    /// This is useful to avoid re-publishing events.
    /// </summary>
    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// Publishes a new domain event to the aggregate root. This event will be stored in the 
    /// <see cref="DomainEvents"/> collection until it is cleared or processed.
    /// </summary>
    /// <param name="domainEvent">The domain event to be published.</param>
    public void PublishEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{T}"/> class.
    /// This constructor is used for deserialization or by derived classes.
    /// </summary>
    protected AggregateRoot()
    {
        _domainEvents = new List<IDomainEvent>();
    }
}
