using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackNucleus.DDD.Domain;

namespace StackNucleus.DDD.Persistence.EF.Postgres.Configurations;

/// <summary>
/// Converts between a custom <see cref="Identity"/> type and a <see cref="Guid"/>.
/// This class is used in Entity Framework to map an <see cref="Identity"/> to a database column of type <see cref="Guid"/>.
/// It facilitates the transformation between the domain-specific identity type and the underlying persistent storage format.
/// </summary>
/// <typeparam name="TIdentity">
/// The type of identity, which must be a subclass of <see cref="Identity"/>.
/// </typeparam>
public class IdentityConverter<TIdentity> : ValueConverter<TIdentity, Guid>
    where TIdentity : Identity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityConverter{TIdentity}"/> class.
    /// The converter maps a custom identity type to a <see cref="Guid"/> and vice versa.
    /// </summary>
    /// <param name="generator">
    /// A function used to generate a new instance of <typeparamref name="TIdentity"/> from a <see cref="Guid"/>.
    /// </param>
    public IdentityConverter(Func<Guid, TIdentity> generator)
        : base(
            identity => identity.Value,  // Convert Identity to Guid (stored value)
            value => generator.Invoke(value)  // Convert Guid to Identity (generated value)
        )
    {
    }
}
