namespace StackNucleus.DDD.Domain.Generators;

/// <summary>
/// Defines the contract for an identifier generator that creates unique identifiers of type <see cref="Identity"/>.
/// The <see cref="IIdGenerator"/> interface allows for the generation of identifiers based on a given <see cref="Guid"/> 
/// and can be customized with different generation strategies.
/// </summary>
public interface IIdGenerator
{
    /// <summary>
    /// Generates a new identifier of the specified type using the provided generation logic.
    /// </summary>
    /// <typeparam name="T">The type of the identifier to generate, which must be a subclass of <see cref="Identity"/>.</typeparam>
    /// <param name="generator">A function that defines how to generate the identifier. The function takes a <see cref="Guid"/> and returns an instance of type <typeparamref name="T"/>.</param>
    /// <returns>A newly generated identifier of type <typeparamref name="T"/>.</returns>
    T Generate<T>(Func<Guid, T> generator) where T : Identity;
}
