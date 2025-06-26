using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackNucleus.DDD.Domain.Outbox;

namespace StackNucleus.DDD.Persistence.EF.Postgres.Configurations.Outbox;

/// <summary>
/// Configures the <see cref="OutboxMessage"/> entity for use with Entity Framework.
/// This class is responsible for defining the schema, primary key, and other configurations 
/// related to the <see cref="OutboxMessage"/> entity within the database context.
/// </summary>
public class OutboxConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    /// <summary>
    /// Configures the <see cref="OutboxMessage"/> entity using the provided <see cref="EntityTypeBuilder{OutboxMessage}"/>.
    /// Defines the table name and primary key for the entity in the database schema.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the <see cref="OutboxMessage"/> entity.
    /// </param>
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(s => s.Id);
    }
}
