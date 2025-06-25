namespace StackNucleus.DDD.Domain.Repositories;

/// <summary>
/// Defines the basic operations for a write repository that works with entities.
/// This interface supports operations such as adding, retrieving, and deleting entities,
/// as well as saving changes to the underlying data store.
/// </summary>
/// <typeparam name="TEntity">The type of entity being managed by the repository.</typeparam>
/// <typeparam name="TIdentifier">The type of identifier for the entity, which must be a subclass of <see cref="Identity"/>.</typeparam>
public interface IBaseWriteRepository<TEntity, TIdentifier>
    where TEntity : IAuditableEntity
    where TIdentifier : Identity
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity, or <c>null</c> if not found.</returns>
    Task<TEntity?> GetById(TIdentifier id);

    /// <summary>
    /// Retrieves a collection of entities by their unique identifiers.
    /// </summary>
    /// <param name="ids">An array of identifiers of the entities to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
    Task<IEnumerable<TEntity>> GetByIds(TIdentifier[] ids);

    /// <summary>
    /// Retrieves a collection of entities by their unique identifiers.
    /// This version accepts <see cref="Guid"/> identifiers.
    /// </summary>
    /// <param name="ids">An array of <see cref="Guid"/> identifiers of the entities to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
    Task<IEnumerable<TEntity>> GetByIds(Guid[] ids);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Add(TEntity entity);

    /// <summary>
    /// Adds multiple new entities to the repository.
    /// </summary>
    /// <param name="entity">The entities to be added.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddRange(params TEntity[] entity);

    /// <summary>
    /// Deletes an existing entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Commits all changes made to the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of affected rows.</returns>
    Task<int> SaveChanges();
}
