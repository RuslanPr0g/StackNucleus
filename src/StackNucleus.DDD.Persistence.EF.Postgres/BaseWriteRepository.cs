using Microsoft.EntityFrameworkCore;
using StackNucleus.DDD.Domain;
using StackNucleus.DDD.Domain.Repositories;

namespace StackNucleus.DDD.Persistence.EF.Postgres;

/// <summary>
/// A base class for write repositories that handle operations such as adding, deleting, and updating entities.
/// This class is intended to be inherited by concrete write repositories that provide methods
/// to manipulate the database for a specific entity type.
/// </summary>
/// <typeparam name="TEntity">
/// The type of the entity that the repository will handle. This should implement <see cref="Entity{TIdentifier}"/>.
/// </typeparam>
/// <typeparam name="TIdentifier">
/// The type of the entity's identifier. This should be a subclass of <see cref="Identity"/>.
/// </typeparam>
/// <typeparam name="TContext">
/// The type of the DbContext used for database operations. This should be a subclass of <see cref="DbContext"/>.
/// </typeparam>
public abstract class BaseWriteRepository<TEntity, TIdentifier, TContext> : IBaseWriteRepository<TEntity, TIdentifier>
    where TEntity : Entity<TIdentifier>
    where TIdentifier : Identity
    where TContext : DbContext
{
    /// <summary>
    /// Gets the <see cref="DbContext"/> instance used for database operations.
    /// </summary>
    protected abstract TContext Context { get; init; }

    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">
    /// The identifier of the entity to retrieve.
    /// </param>
    /// <returns>
    /// The entity with the specified identifier, or <c>null</c> if not found.
    /// </returns>
    public async Task<TEntity?> GetById(TIdentifier id)
    {
        return await Context.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    /// <summary>
    /// Retrieves multiple entities by their identifiers.
    /// </summary>
    /// <param name="ids">
    /// An array of identifiers for the entities to retrieve.
    /// </param>
    /// <returns>
    /// A list of entities with the specified identifiers.
    /// </returns>
    public async Task<IEnumerable<TEntity>> GetByIds(TIdentifier[] ids)
    {
        return await Context.Set<TEntity>()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves multiple entities by their identifiers (specific to <see cref="Guid"/>).
    /// </summary>
    /// <param name="ids">
    /// An array of GUID identifiers for the entities to retrieve.
    /// </param>
    /// <returns>
    /// A list of entities with the specified GUID identifiers.
    /// </returns>
    public async Task<IEnumerable<TEntity>> GetByIds(Guid[] ids)
    {
        return await Context.Set<TEntity>()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new entity to the database.
    /// </summary>
    /// <param name="entity">
    /// The entity to be added.
    /// </param>
    public async Task Add(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    /// <summary>
    /// Adds a range of entities to the database.
    /// </summary>
    /// <param name="entities">
    /// The entities to be added.
    /// </param>
    public async Task AddRange(params TEntity[] entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
    }

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <param name="entity">
    /// The entity to be deleted.
    /// </param>
    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    /// <summary>
    /// Saves changes made to the context to the database asynchronously.
    /// </summary>
    /// <returns>
    /// The number of state entries written to the database.
    /// </returns>
    public Task<int> SaveChanges()
    {
        return Context.SaveChangesAsync();
    }
}