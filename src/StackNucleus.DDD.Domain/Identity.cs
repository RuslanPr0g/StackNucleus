namespace StackNucleus.DDD.Domain;

/// <summary>
/// Represents an identity value object encapsulating a GUID.
/// This is typically used to uniquely identify entities in the domain model.
/// </summary>
public record Identity(Guid Value)
{
    /// <summary>
    /// Implicitly converts a <see cref="Guid"/> to an <see cref="Identity"/>.
    /// </summary>
    /// <param name="identity">The GUID value to be converted.</param>
    /// <returns>An <see cref="Identity"/> instance encapsulating the provided GUID.</returns>
    public static implicit operator Identity(Guid identity) => new(identity);

    /// <summary>
    /// Implicitly converts an <see cref="Identity"/> to a <see cref="Guid"/>.
    /// </summary>
    /// <param name="identity">The <see cref="Identity"/> instance to be converted.</param>
    /// <returns>The <see cref="Guid"/> value encapsulated by the <see cref="Identity"/>.</returns>
    public static implicit operator Guid(Identity identity) => identity.Value;
}
