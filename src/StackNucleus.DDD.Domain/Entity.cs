namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents a base entity in a domain model that includes auditing properties like creation and modification dates.
/// This class can be used as a base class for entities within an aggregate root.
/// </summary>
public abstract class Entity : IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last edited.
    /// </summary>
    public DateTime EditedAt { get; set; }

    /// <summary>
    /// Gets or sets the version of the entity. This can be used for optimistic concurrency control.
    /// </summary>
    public int Version { get; set; }
}

/// <summary>
/// Represents a generic entity with a typed identifier (ID). This entity can be compared with other entities of the same type.
/// It supports equality checks based on the entity's ID and overrides methods for comparison and hash code generation.
/// </summary>
/// <typeparam name="T">
/// The type of the identifier, which must be a subclass of <see cref="Identity"/>.
/// </typeparam>
public abstract class Entity<T> : Entity, IEquatable<Entity<T>> where T : Identity
{
    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    public T Id { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    protected Entity(T id)
    {
        Id = id;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns><c>true</c> if the specified object is equal to the current entity; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<T> other)
            return false;

        return Equals(other);
    }

    /// <summary>
    /// Determines whether the specified entity is equal to the current entity.
    /// </summary>
    /// <param name="other">The entity to compare with the current entity.</param>
    /// <returns><c>true</c> if the specified entity is equal to the current entity; otherwise, <c>false</c>.</returns>
    public bool Equals(Entity<T>? other)
    {
        if (other is null)
            return false;

        return Id.Equals(other.Id);
    }

    /// <summary>
    /// Returns a hash code for the current entity.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// Determines whether two entities of the same type are equal.
    /// </summary>
    /// <param name="left">The left entity to compare.</param>
    /// <param name="right">The right entity to compare.</param>
    /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Entity<T>? left, Entity<T>? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two entities of the same type are not equal.
    /// </summary>
    /// <param name="left">The left entity to compare.</param>
    /// <param name="right">The right entity to compare.</param>
    /// <returns><c>true</c> if the entities are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Entity<T>? left, Entity<T>? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class.
    /// This constructor is typically used by derived classes and may not be used directly.
    /// </summary>
    protected Entity()
    {
    }
}
