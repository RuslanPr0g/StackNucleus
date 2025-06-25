namespace StackNucleus.DDD.Domain.Repositories;

public interface IBaseWriteRepository<TEntity, TIdentifier>
    where TEntity : IAuditableEntity
    where TIdentifier : Identity
{
    Task<TEntity?> GetById(TIdentifier id);
    Task<IEnumerable<TEntity>> GetByIds(TIdentifier[] ids);
    Task<IEnumerable<TEntity>> GetByIds(Guid[] ids);
    Task Add(TEntity entity);
    Task AddRange(params TEntity[] entity);
    void Delete(TEntity entity);

    Task<int> SaveChanges();
}
