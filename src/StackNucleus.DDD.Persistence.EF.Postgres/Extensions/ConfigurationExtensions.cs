using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using StackNucleus.DDD.Domain;
using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;

namespace StackNucleus.DDD.Persistence.EF.Postgres.Extensions;

/// <summary>
/// Provides extension methods for configuring entities with Entity Framework Core.
/// These methods are used to apply common configurations to entities such as versioning, auditing timestamps,
/// and identity handling. The configuration is designed to be reusable across different entity types in a DDD context.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Configures common properties for an entity such as versioning and auditing timestamps.
    /// This method is typically used as a base configuration for entities that require versioning and tracking of creation and modification dates.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity being configured. This type must be a subclass of <see cref="Entity"/>.
    /// </typeparam>
    /// <param name="builder">
    /// The builder used to configure the entity's properties and mappings.
    /// </param>
    /// <returns>
    /// The <see cref="EntityTypeBuilder{TEntity}"/> to allow for further configuration.
    /// </returns>
    public static EntityTypeBuilder<TEntity> ConfigureEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : Entity
    {
        builder.Property(u => u.Version).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.EditedAt).IsRequired();
        return builder;
    }

    /// <summary>
    /// Configures an entity with a custom identity type, including handling the identity's conversion and value generation.
    /// This method is useful when configuring entities with a specific identity type and ensures that the identity
    /// is correctly mapped to the database schema, including setting a default value generator.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity being configured. This type must be a subclass of <see cref="Entity{TIdentity}"/>.
    /// </typeparam>
    /// <typeparam name="TIdentity">
    /// The type of the entity's identity, which must be a subclass of <see cref="Identity"/>.
    /// </typeparam>
    /// <typeparam name="TIdentityConverter">
    /// The type of the <see cref="IdentityConverter{TIdentity}"/> used to convert between the identity type and a database-compatible type.
    /// </typeparam>
    /// <param name="builder">
    /// The builder used to configure the entity's properties and mappings.
    /// </param>
    /// <param name="converter">
    /// An optional custom identity converter to use. If <see langword="null"/>, a default <see cref="IdentityConverter{TIdentity}"/> is used.
    /// </param>
    /// <returns>
    /// The <see cref="EntityTypeBuilder{TEntity}"/> to allow for further configuration.
    /// </returns>
    public static EntityTypeBuilder<TEntity> ConfigureEntity<TEntity, TIdentity, TIdentityConverter>(
        this EntityTypeBuilder<TEntity> builder,
        TIdentityConverter? converter = null)
        where TEntity : Entity<TIdentity>
        where TIdentity : Identity
        where TIdentityConverter : IdentityConverter<TIdentity>, new()
    {
        builder.HasKey(u => u.Id);

        if (converter is null)
        {
            builder.Property(u => u.Id).HasConversion(new TIdentityConverter()).HasValueGenerator<GuidValueGenerator>();
        }
        else
        {
            builder.Property(u => u.Id).HasConversion(converter).HasValueGenerator<GuidValueGenerator>();
        }

        builder.ConfigureEntity();
        return builder;
    }
}