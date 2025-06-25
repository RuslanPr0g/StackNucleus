namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents a value object in the domain-driven design (DDD) context.
/// A value object is an immutable type that is defined only by its value.
/// </summary>
/// <typeparam name="T">
/// The type of the value encapsulated by the value object.
/// </typeparam>
public interface IValueObject<T>
{
    /// <summary>
    /// Gets the value encapsulated by the value object.
    /// </summary>
    T Value { get; }
}
