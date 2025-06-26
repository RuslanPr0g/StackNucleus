using Microsoft.EntityFrameworkCore;
using StackNucleus.DDD.Domain;
using StackNucleus.DDD.Domain.Outbox;
using StackNucleus.DDD.Persistence.EF.Postgres.Configurations.Outbox;

namespace StackNucleus.DDD.Persistence.EF.Postgres;

/// <summary>
/// Represents a base class for DbContexts that implements common auditing, outbox messaging, and schema configuration.
/// This class can be used as the base context for applications requiring a consistent schema and audit tracking.
/// </summary>
public abstract class BaseNucleusContext<TContext> : DbContext
    where TContext : DbContext
{
    /// <inheritdoc cref="DbContext" />
    protected BaseNucleusContext()
    {
    }

    /// <inheritdoc cref="DbContext" />
    protected BaseNucleusContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Gets the schema name to be used for the database context.
    /// Subclasses must implement this property to provide the schema name.
    /// </summary>
    public abstract string SchemaName { get; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{OutboxMessage}"/> representing the outbox messages in the database.
    /// The outbox pattern is used to store domain events for later processing or messaging.
    /// </summary>
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    /// <summary>
    /// Configures the model for the context, including setting the default schema and applying configurations from assembly.
    /// </summary>
    /// <param name="modelBuilder">
    /// The model builder to configure the entities.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(SchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutboxConfiguration).Assembly);
    }

    /// <summary>
    /// Saves changes to the database and sets audit fields for entities that implement <see cref="IAuditableEntity"/>.
    /// </summary>
    /// <returns>
    /// The number of state entries written to the database.
    /// </returns>
    public override int SaveChanges()
    {
        SetAuditFields();
        return base.SaveChanges();
    }

    /// <summary>
    /// Asynchronously saves changes to the database and sets audit fields for entities that implement <see cref="IAuditableEntity"/>.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous save operation, with the number of state entries written to the database.
    /// </returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Sets audit fields (CreatedAt, EditedAt, and Version) for entities that implement <see cref="IAuditableEntity"/>
    /// when their state is added or modified.
    /// </summary>
    private void SetAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditableEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (IAuditableEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.EditedAt = DateTime.UtcNow;
                entity.Version = 1;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.EditedAt = DateTime.UtcNow;
                entity.Version++;
            }
        }
    }
}