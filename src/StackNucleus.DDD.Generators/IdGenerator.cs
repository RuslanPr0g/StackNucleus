using StackNucleus.DDD.Domain;
using StackNucleus.DDD.Domain.Generators;

namespace StackNucleus.DDD.Generators;

/// <inheritdoc cref="IIdGenerator" />
public class IdGenerator : IIdGenerator
{
    /// <inheritdoc cref="IIdGenerator.Generate{T}(Func{Guid, T})" />
    public T Generate<T>(Func<Guid, T> generator) where T : Identity =>
        generator.Invoke(Guid.NewGuid());
}
